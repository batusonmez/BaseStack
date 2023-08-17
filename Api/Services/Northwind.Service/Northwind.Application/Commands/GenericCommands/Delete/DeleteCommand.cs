using MediatRDispatcher;
using Northwind.Application.Models.DTO.Types;

namespace Northwind.Application.Commands.GenericCommands.Delete
{
    /// <summary>
    /// Generic hard delete command
    /// </summary>
    public class DeleteCommand<T> : BaseCommand<DeleteCommandResponse> where T : IDTO
    {
        /// <summary>
        /// Data to proccess
        /// </summary>
        public T Data { get; set; }

        public DeleteCommand(T data)
        {
            Data = data;
        }
    }
}
