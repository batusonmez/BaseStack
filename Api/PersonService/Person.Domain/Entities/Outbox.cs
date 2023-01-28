namespace Person.Domain.Entities
{
    public class Outbox:BaseEntity
    {
        public string? DataType { get; set; }
        public string? Data { get; set; }

        public DateTime? ProcessDate { get; set; }

        public Guid DataID { get; set; }

        
    }
}
