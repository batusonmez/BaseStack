using AutoMapper;
using MediatR;
using Person.Domain.Entities;

namespace Person.Domain.Queries.ListPeople
{
    internal class ListQueryHandler : IRequestHandler<ListPeopleQuery, ListQueryResponse>
    {
        private readonly IMapper mapper;

        public ListQueryHandler(IMapper mapper)
        {
            this.mapper = mapper;
        }
        public Task<ListQueryResponse> Handle(ListPeopleQuery request, CancellationToken cancellationToken)
        {
            var resp=new ListQueryResponse();
            using (var db=new PersonAppContext())
            {
                db.Person.Add(new Person.Domain.Entities.Person()
                {
                    Name="Engin",
                    Surname="Ozdemir",
                    City="Ankara"
                });
                db.SaveChanges();
                 resp.People= db.Person.ToList().Select(d => mapper.Map<ListPeopleDTO>(d));
            }

            return Task.FromResult(resp);
        }
    }
}
