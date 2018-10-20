namespace EasyNote.Model
{
    public class File
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public byte[] Content { get; set; }
        public bool IsLocked { get; set; }
        public string Author { get; set; }
    }
}
