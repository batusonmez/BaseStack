using Person.Domain.DTO;

namespace Person.API.Person.ListPople
{
    public class ListPeopleAPIResponse
    {
        public IEnumerable<PersonDTO> PersonList { get; set; }
        public ListPeopleAPIResponse()
        {
            PersonList = new List<PersonDTO>();
        }
    }
}
