using System.Collections.Generic;
using System.Threading.Tasks;
using EasyNote.Core.Model.DbEntities;
using EasyNote.Core.Model.Files;

namespace EasyNote.Core.Logic.Files
{
    public interface IFilesManager
    {
        Task<IEnumerable<File>> GetFilesListAsync();
        Task<int> AddFileAsync(FileEntity fileParams);
        Task<File> GetFileAsync(int idValue);
        Task UpdateFileAsync(File file, string updatedBy);
        Task DeleteFileAsync(int fileId);
    }
}
