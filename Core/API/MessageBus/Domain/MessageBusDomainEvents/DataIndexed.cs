namespace MessageBusDomainEvents
{
    public class DataIndexed
    {
        public Guid ID { get; set; }
        public string? Name { get; set; }
        public object? Value { get; set; }
    }
}
