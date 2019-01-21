using EasyNote.Core.Model.DbEntities;
using EasyNote.Core.Model.Files;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using EasyNote.Core.Logic.Files;
using Microsoft.AspNetCore.Authorization;
using EasyNote.SignalR;
using Microsoft.AspNetCore.SignalR;

namespace EasyNote.API
{
  [Authorize(Policy = "ApiUser")]
  public class FilesController : Controller
  {
    private readonly IFilesManager _filesManager;
    private readonly IHubContext<EasyNoteHub> _signalRHub;

    public FilesController(IFilesManager filesManager,
                            IHubContext<EasyNoteHub> signalRHub)
    {
      _filesManager = filesManager;
      _signalRHub = signalRHub;
    }

    //GET /files/list
    [HttpGet]
    [ProducesResponseType(200, Type = typeof(IEnumerable<File>))]
    [ProducesResponseType(404)]
    public async Task<IActionResult> List()
    {
      var files = await _filesManager.GetFilesListAsync();
      if (files == null || !files.Any())
        return NotFound();

      return Ok(files);
    }

    //POST /files/create
    [HttpPost]
    [ProducesResponseType(201)]
    [ProducesResponseType(400)]
    public async Task<IActionResult> Create([FromBody] NewFileParams newFileParams)
    {
      if (!ModelState.IsValid)
        return BadRequest("File name and author are required");

      var newFileId = await _filesManager.AddFileAsync(new FileEntity
      {
        Name = newFileParams.Name,
        Author = newFileParams.Author,
        Content = newFileParams.Content
      });

      return Created($"{Request.Scheme}://{Request.Host}/{nameof(Get)}/{newFileId}", newFileId);
    }

    //GET /files/get/{id}
    [HttpGet]
    [ProducesResponseType(200, Type = typeof(File))]
    [ProducesResponseType(400)]
    [ProducesResponseType(404)]
    public async Task<IActionResult> Get([FromRoute] int? id)
    {
      if (!id.HasValue)
        return BadRequest("No file id specified");

      var file = await _filesManager.GetFileAsync(id.Value);
      if (file == null)
        return NotFound(id);

      return Ok(file);
    }

    //PUT /files/update
    [HttpPut]
    [ProducesResponseType(200)]
    [ProducesResponseType(400)]
    public async Task<IActionResult> Update([FromBody] File file)
    {
      if (!ModelState.IsValid)
        return BadRequest("No file id specified");

      var userName = User.FindFirst(ClaimTypes.Name);
      if(userName == null)
        userName = User.FindFirst(ClaimTypes.NameIdentifier);

      await _filesManager.UpdateFileAsync(file, userName.Value);

      return Ok();
    }

    //GET /files/delete/{id}
    [HttpGet]
    [ProducesResponseType(200)]
    [ProducesResponseType(400)]
    public async Task<IActionResult> Delete([FromRoute] int? id)
    {
      if (!id.HasValue)
        return BadRequest("No file id specified");

      await _filesManager.DeleteFileAsync(id.Value);

      return Ok();
    }

    //GET /files/checkout/{id}
    [HttpGet]
    [ProducesResponseType(200)]
    [ProducesResponseType(400)]
    [ProducesResponseType(404)]
    public async Task<IActionResult> Checkout([FromRoute] int? fileId)
    {
      if (!fileId.HasValue)
        return BadRequest("No file id specified");

      var file = await _filesManager.GetFileAsync(fileId.Value);

      if (file == null)
        return NotFound();

      var userName = User.FindFirst(ClaimTypes.Name);
      if (userName == null)
        userName = User.FindFirst(ClaimTypes.NameIdentifier);

      if (file.IsLocked)
      {
        //TODO tutaj poproś o dostęp
        await _signalRHub.Clients.All.SendAsync("UnlockFileRequested",
           new { fileId = file.Id.ToString(), requestor = userName.Value });

        return Ok(new
        {
          canCheckout = false,
          file.ModifiedBy
        });
      }

      file.IsLocked = true;
      await _filesManager.UpdateFileAsync(file, userName.Value);
      //TODO tutaj powiadom o zablokowaniu
      await _signalRHub.Clients.All.SendAsync("FileGotLocked", file.Id.ToString());

      return Ok(
        new
        {
          canCheckout = true,
        });
    }
  }
}
