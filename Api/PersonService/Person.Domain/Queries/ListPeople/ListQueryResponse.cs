
namespace Person.Domain.Queries.ListPeople
{
    public class ListQueryResponse
    {
        public IEnumerable< ListPeopleDTO> People { get; set; }

        public ListQueryResponse()
        {
            People = new List<ListPeopleDTO>();
        }
    }
}
