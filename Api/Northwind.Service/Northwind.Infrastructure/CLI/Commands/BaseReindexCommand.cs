using AutoMapper;
using Northwind.Application.i18n;
using Northwind.Application.Models.DTO;
using Northwind.Application.Models.Exceptions;
using Northwind.Application.Models.Filters.DataQueryFilter;
using Northwind.Application.Services.Outbox;
using Repository;
using System.CommandLine;

namespace Northwind.Infrastructure.CLI.Commands
{
    public class BaseReindexCommand<T, Y> : BaseCommand where T : class where Y : class
    {
        private readonly IMapper mapper;
        private readonly IRepository<T> repository;
        private readonly IOutBoxService outBoxService;
        private readonly IUOW uow;
        public BaseReindexCommand(
            string commandName,
            string commandDesc,
            IMapper mapper,
            IRepository<T> repository,
            IOutBoxService outBoxService,
            IUOW uow
            ) : base(commandName, commandDesc)
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
            NorthwindException.ThrowIf(batch <= 0, CLIResource.InvalidBatchArgument);
            var page = 1;
            while (true)
            {

                Console.WriteLine(CLIResource.InvalidBatchArgument, page);
                DataQuery<T> query = new()
                {
                    Page = page,
                    PageSize = batch
                };
                var entities = repository.GetPaged(query).Select(d => mapper.Map<Y>(d)).Select(d => mapper.Map<OutBoxDTO>(d));
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
