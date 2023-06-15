using AutoMapper;
using MediatR;
using Person.Application.DTO;
using Person.Application.Services.Outbox;
using Repository;

namespace Northwind.Application.Commands.UpsertProduct
{
    public class UpsertProductCommandHandler : IRequestHandler<NewPersonCommand, UpsertProductResponse>
    {
        private readonly IMapper mapper;
        private readonly IRepository<DomainEntities.Person> personRepository;
        private readonly IOutBoxService outBoxService; 
        private readonly IUOW uow;

        public UpsertProductCommandHandler(IMapper mapper,
            IRepository<DomainEntities.Person> personRepository,
            IOutBoxService outBoxService, 
            IUOW uow)
        {
            this.mapper = mapper;
            this.personRepository = personRepository;
            this.outBoxService = outBoxService; 
            this.uow = uow;
        }
        public async Task<NewPersonResponse> Handle(NewPersonCommand request, CancellationToken cancellationToken)
        {

            var entity = mapper.Map<DomainEntities.Person>(request.Person);
            personRepository.Insert(entity);
            var personDTO = mapper.Map<PersonDTO>(entity);
            var resp = new NewPersonResponse(personDTO);
            resp.ID = entity.ID;
            outBoxService.SaveOutBox(mapper.Map<OutBoxDTO>(personDTO));
            await uow.Save();
            return resp;
        }


    }
}
