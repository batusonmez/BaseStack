namespace MessageBusDomainEvents
{
    public class IndexData
    {
        public Guid? OutboxID { get; set; }
        public string ID { get; set; }
        public string? Name { get; set; }    
        public object? Value { get; set; }
    }
}
