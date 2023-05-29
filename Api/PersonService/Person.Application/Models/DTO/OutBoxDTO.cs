namespace Person.Application.DTO
{
    public class OutBoxDTO
    {
        public Guid ID { get; set; }
        public string? DataType { get; set; }
        public object? Data { get; set; }
    }
}
