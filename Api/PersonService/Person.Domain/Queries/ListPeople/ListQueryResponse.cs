
namespace Person.Domain.Queries.ListPeople
{
    public class ListQueryResponse
    {
        public IEnumerable<Person.Domain.Entities.Person> People { get; set; }

        public ListQueryResponse()
        {
            People = new List<Person.Domain.Entities.Person>();
        }
    }
}
