using System.ComponentModel.DataAnnotations;

namespace EasyNote.Core.Model
{
    public class NewFileParams
    {
        public string Name { get; set; }
        public byte[] Content { get; set; }
        public string Author { get; set; }
    }
}
