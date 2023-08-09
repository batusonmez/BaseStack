using System.Text.Json.Serialization;

namespace Northwind.Application.Models.DTO
{
    public class SupplierDTO : BaseDTO
    {
        public int SupplierId { get; set; }
        public string CompanyName { get; set; }
        public string ContactName { get; set; }
        public string ContactTitle { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string Region { get; set; }
        public string PostalCode { get; set; }
        public string Country { get; set; }
        public string Phone { get; set; }
        public string Fax { get; set; }
        public string HomePage { get; set; }

        [JsonIgnore]
        public override bool HasID => SupplierId>0;
        [JsonIgnore]
        public override bool IndexEnabled =>true;
        [JsonIgnore]
        public override object IndexKey => SupplierId;

        public SupplierDTO()
        {
            
        }
    }
}
