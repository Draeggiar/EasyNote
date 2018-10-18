namespace EasyNote.Model.DbEntities
{
    public class FileEntity
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public byte[] Content { get; set; }
        public string Author { get; set; }
    }
}
