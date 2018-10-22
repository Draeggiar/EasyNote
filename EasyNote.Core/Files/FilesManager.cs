using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EasyNote.Core.Files.Interfaces;
using EasyNote.Core.Model;
using EasyNote.Core.Model.DbEntities;
using Microsoft.EntityFrameworkCore;

namespace EasyNote.Core.Files
{
    public class FilesManager : IFilesManager
    {
        private readonly FilesDbContext _dbContext;

        public FilesManager(IDbContext dbContext)
        {
            _dbContext = dbContext as FilesDbContext;
        }

        public async Task<IEnumerable<File>> GetFilesListAsync()
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

        public async Task<FileEntity> AddFile(NewFileParams fileParams)
        {
            var newFile = _dbContext.Files.Add(new FileEntity
            {
                Name = fileParams.Name,
                Author = fileParams.Author,
                Content = fileParams.Content
            }).Entity;

            if(await _dbContext.SaveChangesAsync() < 1)
                throw new InvalidOperationException("Could not add file to database");

            return newFile;
        }
    }
}
