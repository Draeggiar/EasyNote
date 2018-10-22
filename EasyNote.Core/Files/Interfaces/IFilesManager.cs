using System.Collections.Generic;
using System.Threading.Tasks;
using EasyNote.Core.Model;
using EasyNote.Core.Model.DbEntities;

namespace EasyNote.Core.Files.Interfaces
{
    public interface IFilesManager
    {
        Task<IEnumerable<File>> GetFilesListAsync();
        Task<FileEntity> AddFile(NewFileParams fileParams);
    }
}
