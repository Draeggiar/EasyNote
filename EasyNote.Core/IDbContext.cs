using EasyNote.Core.Model.DbEntities;
using Microsoft.EntityFrameworkCore;

namespace EasyNote.Core
{
    public interface IDbContext
    {
        DbSet<FileEntity> Files { get; set; }
    }
}
