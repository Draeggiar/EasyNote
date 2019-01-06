using EasyNote.Core.Logic.Files;
using EasyNote.Core.Model.Files;
using Microsoft.AspNetCore.SignalR;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EasyNote.SignalR
{
  public class EasyNoteHub : Hub
  {
    private readonly IFilesManager filesManager;

    public EasyNoteHub(IFilesManager filesManager)
    {
      this.filesManager = filesManager;
    }
    public async Task LockFile(string file)
    {
      var deserialized = JsonConvert.DeserializeObject<File>(file);

      deserialized.IsLocked = true;

      await filesManager.UpdateFileAsync(deserialized);

      await Clients.All.SendAsync("FileGotLocked", deserialized.Id);
    }

    public async Task UnlockFile(string file)
    {
      var deserialized = JsonConvert.DeserializeObject<File>(file);

      deserialized.IsLocked = false;

      await filesManager.UpdateFileAsync(deserialized);

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
