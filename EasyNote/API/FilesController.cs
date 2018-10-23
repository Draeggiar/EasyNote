using EasyNote.Core.Files.Interfaces;
using EasyNote.Core.Model;
using EasyNote.Core.Model.DbEntities;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EasyNote.Core.Model.Files;

namespace EasyNote.API
{
    public class FilesController : Controller
    {
        private readonly IFilesManager _filesManager;

        public FilesController(IFilesManager filesManager)
        {
            _filesManager = filesManager;
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

            var newFileId = await _filesManager.AddFile(new FileEntity
            {
                Name = newFileParams.Name,
                Author = newFileParams.Author,
                Content = newFileParams.Content
            });

            return Created($"{Request.Scheme}://{Request.Host}/{nameof(Get)}/{newFileId}", null);
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
    }
}
