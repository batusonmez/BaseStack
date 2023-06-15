﻿using MediatRDispatcher;
using Northwind.Application.Models.DTO.Types;

namespace Northwind.Application.Commands
{
    /// <summary>
    /// Generic update or insert command
    /// </summary>
    public class UpsertCommand<T> : BaseCommand<UpsertCommandResponse> where T : IDTO
    {
        /// <summary>
        /// Data to proccess
        /// </summary>
        public T Data { get; set; }

        public UpsertCommand(T data)
        {
            Data = data;
        }
    }
}
