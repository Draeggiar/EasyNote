using System.Collections.Generic;
using System.Threading.Tasks;
using EasyNote.Core.Files.Interfaces;
using EasyNote.Core.Model;
using Microsoft.AspNetCore.Mvc;

namespace EasyNote.API
{
    public class FilesController : Controller
    {
        private readonly IFilesManager _filesManager;

        public FilesController(IFilesManager filesManager)
        {
            _filesManager = filesManager;
        }

        //GET /Files/List
        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<File>))]
        [ProducesResponseType(404)]
        public async Task<IActionResult> List()
        {
            var files = await _filesManager.GetFilesListAsync();
            if (files == null)
                return NotFound();

            return Ok(files);
        }

        //POST Files/AddNew
        [HttpPost]
        public async Task<IActionResult> AddNew([FromBody] NewFileParams newFileParams)
        {
            var result = await _filesManager.AddFile(newFileParams);
            return Ok(result);
        }
    }
}
