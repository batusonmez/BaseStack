
namespace Northwind.Application.Models.DTO
{
    public class OutBoxDTO
    { 
        public Guid ID { get; set; }
        public string? DataID { get; set; }
        public string? DataType { get; set; }
        public object? Data { get; set; }
    }
}
