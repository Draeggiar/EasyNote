using EasyNote.Model.DbEntities;
using Microsoft.EntityFrameworkCore;

namespace EasyNote
{
    public class FilesDbContext : DbContext
    {
        public DbSet<FileEntity> Files { get; set; }

        public FilesDbContext(DbContextOptions options) : base(options)
        {
        }
    }
}
