namespace ArceliaHR.Models
{
    public class FileModel
    {
        public int Id { get; set; }
        public string FileName { get; set; } = "";
        public string Extension { get; set; } = "";
        public string? MimeType { get; set; }
        public long FileSize { get; set; }
        public byte[] Data { get; set; } = Array.Empty<byte>();
        public DateTime CreatedAt { get; set; }
    }
}

