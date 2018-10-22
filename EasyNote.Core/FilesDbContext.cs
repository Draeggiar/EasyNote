using EasyNote.Core.Model.DbEntities;
using Microsoft.EntityFrameworkCore;

namespace EasyNote.Core
{
    public class FilesDbContext : DbContext, IDbContext
    {
        public DbSet<FileEntity> Files { get; set; }

        public FilesDbContext(DbContextOptions options) : base(options)
        {
        }
    }
}
