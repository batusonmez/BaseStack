using AutoMapper;
using MediatR;
using Person.Application.DTO;
using Person.Application.Models.Exceptions;
using Person.Application.Services.Outbox;
using Repository;

namespace Person.Application.Commands.NewPerson
{
    public class ReIndexPeopleCommandHandler : IRequestHandler<ReIndexPeopleCommand, ReIndexPeopleResponse>
    {
        private readonly IMapper mapper;
        private readonly IRepository<DomainEntities.Person> personRepository;
        private readonly IOutBoxService outBoxService; 
        private readonly IUOW uow;

        public ReIndexPeopleCommandHandler(IMapper mapper,
            IRepository<DomainEntities.Person> personRepository,
            IOutBoxService outBoxService, 
            IUOW uow)
        {
            this.mapper = mapper;
            this.personRepository = personRepository;
            this.outBoxService = outBoxService; 
            this.uow = uow;
        }
        public async Task<ReIndexPeopleResponse> Handle(ReIndexPeopleCommand request, CancellationToken cancellationToken)
        {

            //var respose = new ReIndexPeopleResponse();
            //var entities = personRepository.GetPaged(1, 100000).Select(d => mapper.Map<PersonDTO>(d)).Select(d => mapper.Map<OutBoxDTO>(d));
            //outBoxService.SaveOutBox(entities);                          
            //await uow.Save();

            //return respose;
            return null;
        }

    }
}
