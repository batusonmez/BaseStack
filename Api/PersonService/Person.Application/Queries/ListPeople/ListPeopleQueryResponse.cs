
using MediatRDispatcher.Query;

namespace Person.Application.Queries.ListPeople
{
    public class ListPeopleQueryResponse : BaseQueryResult<ListPeopleDTO>
    {

        public ListPeopleQueryResponse(IEnumerable<ListPeopleDTO> data, int total) : base(data)
        {

        }

    }
}
