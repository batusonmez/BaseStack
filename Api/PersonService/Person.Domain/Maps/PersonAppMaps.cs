using AutoMapper;
using Person.Domain.Queries.ListPeople;

namespace Person.Domain.Maps
{


    public class PersonAppProfile : Profile
    {
        public PersonAppProfile()
        {
            CreateMap<Person.Domain.Entities.Person, ListPeopleDTO>();
        }
    }
}
