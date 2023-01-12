using MediatR;

namespace Person.Domain.Queries.ListPeople
{
    internal class ListQueryHandler : IRequestHandler<ListPeopleQuery, ListQueryResponse>
    {
        public Task<ListQueryResponse> Handle(ListPeopleQuery request, CancellationToken cancellationToken)
        {
            return Task.FromResult(new ListQueryResponse());
        }
    }
}
