using System.Collections.Generic;
using System.Threading.Tasks;
using EasyNote.Core.Model;
using EasyNote.Core.Model.DbEntities;
using EasyNote.Core.Model.Files;

namespace EasyNote.Core.Files.Interfaces
{
    public interface IFilesManager
    {
        Task<IEnumerable<File>> GetFilesListAsync();
        Task<int> AddFile(FileEntity fileParams);
        Task<File> GetFileAsync(int idValue);
    }
}
