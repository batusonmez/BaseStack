using AutoMapper;
using MediatR;
using Person.Domain.Entities;
using Repository;

namespace Person.Domain.Queries.ListPeople
{
    internal class ListQueryHandler : IRequestHandler<ListPeopleQuery, ListQueryResponse>
    {
        private readonly IMapper mapper;
        private readonly IRepository<Entities.Person> personRepository;

        public ListQueryHandler(IMapper mapper,
            IRepository<Entities.Person> personRepository)
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
