using Northwind.Application.Models.DTO.Types;

namespace Northwind.Application.Commands.GenericCommands.Delete
{
    /// <summary>
    /// Generic basic hard delete response model
    /// </summary>
    public class DeleteCommandResponse
    {
        /// <summary>
        /// Response Data
        /// </summary>
        public IDTO Data { get; set; }

        public DeleteCommandResponse(IDTO data)
        {
            Data = data;
        }

    }
}
