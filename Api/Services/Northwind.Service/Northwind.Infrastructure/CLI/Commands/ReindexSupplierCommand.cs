using AutoMapper;
using Northwind.Application.i18n;
using Northwind.Application.Models.DTO;
using Northwind.Application.Services.Outbox;
using Northwind.Domain.Entities;
using Repository;

namespace Northwind.Infrastructure.CLI.Commands
{
    public class ReindexSupplierCommand : BaseReindexCommand<Supplier,SupplierDTO>
    {
       

        public ReindexSupplierCommand(IMapper mapper,
            IRepository<Supplier> repository,
            IOutBoxService outBoxService,
            IUOW uow) : base("rxsupplier", CLIResource.CommandDescReindexCategory, mapper, repository, outBoxService, uow)
        {


        }

    }
}
