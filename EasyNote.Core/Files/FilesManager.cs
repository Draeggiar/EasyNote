using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using EasyNote.Core.Files.Interfaces;
using EasyNote.Core.Model;
using EasyNote.Core.Model.DbEntities;
using EasyNote.Core.Model.Files;
using Microsoft.EntityFrameworkCore;

namespace EasyNote.Core.Files
{
    public class FilesManager : IFilesManager
    {
        private readonly IMapper _mapper;
        private readonly FilesDbContext _dbContext;

        public FilesManager(IDbContext dbContext, IMapper mapper)
        {
            _mapper = mapper;
            _dbContext = dbContext as FilesDbContext;
        }

        public async Task<IEnumerable<File>> GetFilesListAsync()
        {
            var filesFromDb = await _dbContext.Files.ToListAsync();

            return filesFromDb.Select(f => _mapper.Map<File>(f));
        }

        public async Task<int> AddFile(FileEntity fileEntity)
        {
            var newFileId = _dbContext.Files.Add(fileEntity).Entity.Id;

            if(await _dbContext.SaveChangesAsync() < 1)
                throw new InvalidOperationException("Could not add file to database");

            return newFileId;
        }

        public async Task<File> GetFileAsync(int idValue)
        {
            var fileEntity = await _dbContext.Files.FindAsync(idValue);

            return fileEntity == null ? null : _mapper.Map<File>(fileEntity);
        }
    }
}
