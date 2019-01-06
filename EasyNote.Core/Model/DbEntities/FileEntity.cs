using System.ComponentModel.DataAnnotations;

namespace EasyNote.Core.Model.DbEntities
{
    public class FileEntity
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Content { get; set; }
        public string Author { get; set; }
        public bool IsLocked { get; set; }
    }
}
