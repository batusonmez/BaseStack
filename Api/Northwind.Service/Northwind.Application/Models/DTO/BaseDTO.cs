using Northwind.Application.Models.DTO.Types;
using System.Text.Json.Serialization;

namespace Northwind.Application.Models.DTO
{
    public abstract class BaseDTO : IDTO
    {
        [JsonIgnore]
        public virtual object IndexKey { get; }=string.Empty;

        [JsonIgnore]
        public virtual bool IndexEnabled { get; }

        [JsonIgnore]
        public virtual bool HasID { get; }

        public virtual object? ID
        {
            get
            {
                return null;
            }
        }
    }
}
