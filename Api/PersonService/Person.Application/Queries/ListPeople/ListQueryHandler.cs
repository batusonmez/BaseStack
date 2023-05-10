using AutoMapper;
using MediatR; 
using Repository;

namespace Person.Application.Queries.ListPeople
{
    internal class ListQueryHandler : IRequestHandler<ListPeopleQuery, ListQueryResponse>
    {
        private readonly IMapper mapper;
        private readonly IRepository<DomainEntities.Person> personRepository;

        public ListQueryHandler(IMapper mapper,
            IRepository<DomainEntities.Person> personRepository)
        {
            this.mapper = mapper;
            this.personRepository = personRepository;
        }
        public Task<ListQueryResponse> Handle(ListPeopleQuery request, CancellationToken cancellationToken)
        {
            var resp = new ListQueryResponse(); 
            resp.People = personRepository.Get().Select(d => mapper.Map<ListPeopleDTO>(d));
            return Task.FromResult(resp);
        }
    }
}
