using System.Text.Json.Serialization;

namespace Northwind.Application.Models.DTO
{
    public class CategoryDTO : BaseDTO
    {
        public int CategoryId { get; set; }
        public string CategoryName { get; set; }
        public string Description { get; set; }

        [JsonIgnore]
        public override bool HasID => CategoryId > 0;

        [JsonIgnore]
        public override bool IndexEnabled => true;

        [JsonIgnore]
        public override object IndexKey => CategoryId;

        public CategoryDTO()
        {
            CategoryId = 0;
            
        }
    }
}
