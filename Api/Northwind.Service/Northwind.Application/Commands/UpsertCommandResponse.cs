using Northwind.Application.Models.DTO.Types;

namespace Northwind.Application.Commands
{
    /// <summary>
    /// Generic basic update or insert command response model
    /// </summary>
    public class UpsertCommandResponse 
    {
        /// <summary>
        /// Response Data
        /// </summary>
        public IDTO Data { get; set; }

        public UpsertCommandResponse(IDTO data)
        {
            Data = data;
        }

    }
}
