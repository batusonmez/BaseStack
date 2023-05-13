using AutoMapper;
using MediatR;
using Repository;

namespace Person.Application.Queries.ListPeople
{
    internal class ListPeopleQueryHandler : IRequestHandler<ListPeopleQuery, ListPeopleQueryResponse>
    {
        private readonly IMapper mapper;
        private readonly IRepository<DomainEntities.Person> personRepository;

        public ListPeopleQueryHandler(IMapper mapper,
            IRepository<DomainEntities.Person> personRepository)
        {
            this.mapper = mapper;
            this.personRepository = personRepository;
        }
        public Task<ListPeopleQueryResponse> Handle(ListPeopleQuery request, CancellationToken cancellationToken)
        {
            var data = personRepository.GetPaged(request.Page, request.PageSize).Select(d => mapper.Map<ListPeopleDTO>(d));
            var resp = new ListPeopleQueryResponse(data);
            return Task.FromResult(resp);
        }
    }
}
