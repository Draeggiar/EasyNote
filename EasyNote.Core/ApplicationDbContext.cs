using EasyNote.Core.Model.DbEntities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace EasyNote.Core
{
    public class ApplicationDbContext : IdentityDbContext<UserEntity>, IDbContext
    {
        public DbSet<FileEntity> Files { get; set; }

        public ApplicationDbContext(DbContextOptions options) : base(options)
        {
        }
    }
}
