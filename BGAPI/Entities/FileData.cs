namespace BGAPI.Entities
{
    public record FileData
    {
        public FileData(byte[] data, string name, string contentDisposition) =>
            (Data, Name, ContentType) = (data, name, contentDisposition);
        public byte[] Data { get; }
        public string Name { get; }
        public string ContentType { get; }
    }
}