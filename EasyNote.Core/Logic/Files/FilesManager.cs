using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using EasyNote.Core.Model.DbEntities;
using EasyNote.Core.Model.Files;
using Microsoft.EntityFrameworkCore;

namespace EasyNote.Core.Logic.Files
{
    public class FilesManager : IFilesManager
    {
        private readonly IMapper _mapper;
        private readonly ApplicationDbContext _dbContext;

        public FilesManager(IDbContext dbContext, IMapper mapper)
        {
            _mapper = mapper;
            _dbContext = dbContext as ApplicationDbContext;
        }

        public async Task<IEnumerable<File>> GetFilesListAsync()
        {
            var filesFromDb = await _dbContext.Files.ToListAsync();

            return filesFromDb.Select(f => _mapper.Map<File>(f));
        }

        public async Task<int> AddFileAsync(FileEntity fileEntity)
        {
            var newFileId = _dbContext.Files.Add(fileEntity).Entity.Id;

            if (await _dbContext.SaveChangesAsync() < 1)
                throw new InvalidOperationException("Could not add file to database");

            return newFileId;
        }

        public async Task<File> GetFileAsync(int idValue)
        {
            var fileEntity = await _dbContext.Files.FindAsync(idValue);

            return fileEntity == null ? null : _mapper.Map<File>(fileEntity);
        }

        public async Task UpdateFileAsync(File file)
        {
            var fileEntity = _mapper.Map<FileEntity>(file);

            //TODO TB Wydajność - sprawdzanie czy encja faktycznie się zmieniła
            _dbContext.Entry(fileEntity).State = EntityState.Modified;
            await _dbContext.SaveChangesAsync();
        }

        public async Task DeleteFileAsync(int fileId)
        {
            var fileEntity = await _dbContext.Files.FindAsync(fileId);

            _dbContext.Files.Remove(fileEntity);
            await _dbContext.SaveChangesAsync();
        }
    }
}
