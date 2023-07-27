using AutoMapper;
using Person.Application.DTO;
using Person.Application.Services.Outbox;
using Repository;
using System.CommandLine;
using System.Diagnostics;

namespace Person.Infrastructure.CLI.Commands
{
    public class IndexCommand : BaseCommand
    {
        private readonly IMapper mapper;
        private readonly IRepository<DomainEntities.Person> personRepository;
        private readonly IOutBoxService outBoxService;
        private readonly IUOW uow;

        public IndexCommand(IMapper mapper,
            IRepository<DomainEntities.Person> personRepository,
            IOutBoxService outBoxService,
            IUOW uow) : base("reindexpeople", "reindex person table")
        {

            var createOption = new Option<int>(
              name: "--batch",
              description: "batch size");
            AddOption(createOption);

            this.SetHandler(async (batch) =>
            {
                await Handle(batch);
            }, createOption);
            this.mapper = mapper;
            this.personRepository = personRepository;
            this.outBoxService = outBoxService;
            this.uow = uow;
        }

        async Task Handle(int batch)
        {

            var page = 1;
            //while (true)
            //{

            //    Console.WriteLine($"{page}. Indexing.");
            //    var entities = personRepository.GetPaged(page, batch).Select(d => mapper.Map<PersonDTO>(d)).Select(d => mapper.Map<OutBoxDTO>(d));
            //    if (!entities.Any())
            //    {

            //        break;
            //    }

            //    outBoxService.SaveOutBox(entities);
            //    await uow.Save();


            //    page++;

            //}

        }
    }
}
