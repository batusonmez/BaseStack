using AutoMapper;
using Person.Domain.Commands.NewPerson;
using Person.Domain.DTO;
using Person.Domain.Queries.ListPeople;

namespace Person.Domain.Maps
{
    public class PersonAppProfile : Profile
    {
        public PersonAppProfile()
        {
            CreateMap<Entities.Person, ListPeopleDTO>();
            CreateMap<Entities.Person, PersonDTO>(); 
            CreateMap<PersonDTO, Entities.Person>();
        }
    }
}
