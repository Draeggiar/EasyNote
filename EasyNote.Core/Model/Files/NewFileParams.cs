using System.ComponentModel.DataAnnotations;

namespace EasyNote.Core.Model.Files
{
    public class NewFileParams
    {
        [Required]
        public string Name { get; set; }
        [Required]
        public string Author { get; set; }
        public string Content { get; set; }
    }
}
