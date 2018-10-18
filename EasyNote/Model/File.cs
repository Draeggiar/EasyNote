namespace EasyNote.Model
{
    public class File
    {
        public string Name { get; set; }
        public byte[] Content { get; set; }
        public bool IsLocked { get; set; }
    }
}
