using System.Text.Json.Serialization;

namespace Northwind.Application.Models.DTO
{
    public class ProductsDTO : BaseDTO
    {
        public int ProductId { get; set; }
        public string? ProductName { get; set; }
        public int? SupplierId { get; set; }
        public int? CategoryId { get; set; }
        public string? QuantityPerUnit { get; set; }
        public decimal? UnitPrice { get; set; }
        public short? UnitsInStock { get; set; }
        public short? UnitsOnOrder { get; set; }
        public short? ReorderLevel { get; set; }
        public bool Discontinued { get; set; }
        public string? CategoryName { get; set; }
        public string? SupplierName { get; set; }


        [JsonIgnore]
        public override object IndexKey => ProductId;
        [JsonIgnore]
        public override bool IndexEnabled => true;
        [JsonIgnore]
        public override bool HasID => ProductId > 0;
    }
}
