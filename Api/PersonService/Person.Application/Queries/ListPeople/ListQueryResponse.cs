
namespace Person.Application.Queries.ListPeople
{
    public class ListQueryResponse
    {
        public IEnumerable<ListPeopleDTO> People { get; set; }

        public ListQueryResponse()
        {
            People = new List<ListPeopleDTO>();
        }
    }
}
