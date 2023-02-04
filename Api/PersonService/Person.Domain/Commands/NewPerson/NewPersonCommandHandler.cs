using AutoMapper;
using MediatR;
using Person.Domain.DTO;
using Person.Domain.Entities;
using Person.Domain.Services.Outbox;
using Repository;

namespace Person.Domain.Commands.NewPerson
{
    internal class NewPersonCommandHandler : IRequestHandler<NewPersonCommand, NewPersonResponse>
    {
        private readonly IMapper mapper;
        private readonly IRepository<Entities.Person> personRepository;
        private readonly IOutBoxService outBoxService;
        private readonly IUOW uow;

        public NewPersonCommandHandler(IMapper mapper,
            IRepository<Entities.Person> personRepository,
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
            
            var entity = mapper.Map<Entities.Person>(request.Person);
            personRepository.Insert(entity);
            var personDTO = mapper.Map<PersonDTO>(entity);
            var resp = new NewPersonResponse(personDTO);
            outBoxService.SaveOutBox(new OutBoxDTO()
            {
                Data = personDTO,
                ID = entity.ID,
                DataType = personDTO.IndexName()                
            });
            await uow.Save(); 
            return resp;
        }


    }
}
