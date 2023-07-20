using System.Reflection.Metadata.Ecma335;

namespace Northwind.Application.Models.Filters
{
    public class Filter
    {
        public string? Keyword { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public int? Quantity { get; set; }

    }
}
