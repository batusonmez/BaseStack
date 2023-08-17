using System.Text.Json.Serialization;

namespace Northwind.Application.Models.DTO
{
    public class CustomersDTO : BaseDTO
    {
        public string CustomerId { get; set; }
        public string? CompanyName { get; set; }
        public string? ContactName { get; set; }
        public string? ContactTitle { get; set; }
        public string? Address { get; set; }
        public string? City { get; set; }
        public string? Region { get; set; }
        public string? PostalCode { get; set; }
        public string? Country { get; set; }
        public string? Phone { get; set; }
        public string? Fax { get; set; }

        [JsonIgnore]
        public override bool HasID => !string.IsNullOrEmpty(CustomerId);
        [JsonIgnore]
        public override bool IndexEnabled =>true;
        [JsonIgnore]
        public override object IndexKey => CustomerId;

        public CustomersDTO()
        {
            CustomerId = string.Empty;
        }
    }
}
