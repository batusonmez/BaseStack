using AutoMapper;
using EFAdapter.Models;
using Exceptions;
using Northwind.Application.i18n;
using Northwind.Application.Models.DTO;
using Northwind.Application.Models.Filters.DataQueryFilter;
using Northwind.Application.Services.Outbox;
using Northwind.Domain.Entities;
using Repository;
using System.CommandLine;

namespace Northwind.Infrastructure.CLI.Commands
{
    public class ReindexCategoriesCommand : BaseCommand
    {
        private readonly IMapper mapper;
        private readonly IRepository<Category> repository;
        private readonly IOutBoxService outBoxService;
        private readonly IUOW uow;

        public ReindexCategoriesCommand(IMapper mapper,
            IRepository<Category> repository,
            IOutBoxService outBoxService,
            IUOW uow) : base("reindexcategory", CLIResource.CommandDescReindexCategory)
        {

            var createOption = new Option<int>(
              name: "--batch",
              description: CLIResource.ParamDescBatch);
            AddOption(createOption);


            this.SetHandler(async (batch) =>
            {
                await Handle(batch);
            }, createOption);
            this.mapper = mapper;
            this.repository = repository;
            this.outBoxService = outBoxService;
            this.uow = uow;
        }

        public async Task Handle(int batch)
        {
            BaseException.ThrowIf(batch <= 0, CLIResource.InvalidBatchArgument);
            var page = 1;
            while (true)
            {

                Console.WriteLine(CLIResource.InvalidBatchArgument, page);
                DataQuery<Category> query = new()
                {
                    Page = page,
                    PageSize = batch
                };
                var entities = repository.GetPaged(query).Select(d => mapper.Map<CategoryDTO>(d)).Select(d => mapper.Map<OutBoxDTO>(d));
                if (!entities.Any())
                {

                    break;
                }
                outBoxService.SaveOutBox(entities);
                await uow.Save();
                page++;

            }

        }
    }
}
