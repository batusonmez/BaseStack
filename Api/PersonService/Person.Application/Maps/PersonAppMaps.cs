using AutoMapper;
using Person.Application.DTO;
using Person.Application.Queries.ListPeople;

namespace Person.Application.Maps
{
    public class PersonAppProfile : Profile
    {
        public PersonAppProfile()
        {
            CreateMap<DomainEntities.Person, ListPeopleDTO>();
            CreateMap<DomainEntities.Person, PersonDTO>(); 
            CreateMap<PersonDTO, DomainEntities.Person>();
        }
    }
}
