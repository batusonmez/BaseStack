namespace Northwind.Domain.Entities
{
    public class Outbox:BaseEntity
    {
        public string? DataType { get; set; }
        public string? Data { get; set; }
        public DateTime? RequestDate { get; set; }
        public DateTime? ProcessDate { get; set; }
        public string DataID { get; set; }  
        public Outbox()
        {
            DataID =string.Empty;
        }

        
    }
}
