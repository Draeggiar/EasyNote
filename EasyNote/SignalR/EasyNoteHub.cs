using EasyNote.Core.Logic.Files;
using EasyNote.Core.Model.Files;
using Microsoft.AspNetCore.SignalR;
using Newtonsoft.Json;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace EasyNote.SignalR
{
  public class EasyNoteHub : Hub
  {
    private readonly IFilesManager _filesManager;
    private readonly IHttpContextAccessor _httpContextAccessor;

    public EasyNoteHub(IFilesManager filesManager, IHttpContextAccessor httpContextAccessor)
    {
      _filesManager = filesManager;
      _httpContextAccessor = httpContextAccessor;
    }
    public async Task LockFile(string file)
    {
      var deserialized = JsonConvert.DeserializeObject<File>(file);

      deserialized.IsLocked = true;

      await _filesManager.UpdateFileAsync(deserialized, _httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.Name));

      await Clients.All.SendAsync("FileGotLocked", deserialized.Id);
    }

    public async Task UnlockFile(string file)
    {
      var deserialized = JsonConvert.DeserializeObject<File>(file);

      deserialized.IsLocked = false;

      await _filesManager.UpdateFileAsync(deserialized, _httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.Name));

      await Clients.All.SendAsync("FileGotUnlocked", deserialized.Id);
    }

    public async Task RequestUnlockingFile(string fileId, string requestor)
    {
      await Clients.All.SendAsync("UnlockFileRequested", fileId, requestor);
    }

    public async Task ForbidUnlockingFile(string fileId, string forbidder)
    {
      await Clients.All.SendAsync("UnlockingFileForbidden", fileId, forbidder);
    }

  }
}
