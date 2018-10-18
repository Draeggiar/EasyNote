using EasyNote.Model.DbEntities;
using Microsoft.EntityFrameworkCore;

namespace EasyNote
{
    public class FilesContext : DbContext
    {
        public DbSet<FileEntity> Files { get; set; }

        public FilesContext(DbContextOptions options) : base(options)
        {
        }
    }
}
