using AutoMapper;
using MediatR;
using Repository;

namespace Person.Application.Queries.ListPeople
{
    public class ListPeopleQueryHandler : IRequestHandler<ListPeopleQuery, ListPeopleQueryResponse>
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

            //var query = personRepository.GetPaged(request.Page, request.PageSize);
            //var data = query.Select(d => mapper.Map<ListPeopleDTO>(d));
            //var resp = new ListPeopleQueryResponse(data, query.Total); 
            //return Task.FromResult(resp);
            return null;
        }
    }
}
