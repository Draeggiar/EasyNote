using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EasyNote.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EasyNote.API
{
    public class FilesController : Controller
    {
        private readonly FilesDbContext _dbContext;

        public FilesController(FilesDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        [HttpGet]
        public async Task<IEnumerable<File>> List()
        {
            var filesFromDb = await _dbContext.Files.ToListAsync();

            return filesFromDb.Select(f => new File
            {
                Id = f.Id,
                Name = f.Name,
                Author = f.Author,
                IsLocked = false
            });
        }
    }
}
