﻿using EasyNote.Core.Model.DbEntities;
using EasyNote.Core.Model.Files;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EasyNote.Core.Files.Interfaces
{
    public interface IFilesManager
    {
        Task<IEnumerable<File>> GetFilesListAsync();
        Task<int> AddFileAsync(FileEntity fileParams);
        Task<File> GetFileAsync(int idValue);
        Task UpdateFileAsync(File file);
        Task DeleteFileAsync(int fileId);
    }
}
