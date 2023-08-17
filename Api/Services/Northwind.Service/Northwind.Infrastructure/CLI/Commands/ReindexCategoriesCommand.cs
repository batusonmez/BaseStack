using AutoMapper;
using EFAdapter.Models;
using Exceptions;
using Northwind.Application.i18n;
using Northwind.Application.Models.DTO;
using Northwind.Application.Models.Exceptions;
using Northwind.Application.Models.Filters.DataQueryFilter;
using Northwind.Application.Services.Outbox;
using Northwind.Domain.Entities;
using Repository;
using System.CommandLine;

namespace Northwind.Infrastructure.CLI.Commands
{
    public class ReindexCategoriesCommand : BaseReindexCommand<Category,CategoryDTO>
    {
 
        public ReindexCategoriesCommand(IMapper mapper,
            IRepository<Category> repository,
            IOutBoxService outBoxService,
            IUOW uow) : base("rxcategory", CLIResource.CommandDescReindexCategory, mapper, repository, outBoxService, uow)
        {


        }

    }
}
