using AutoMapper;
using Newtonsoft.Json;
using Person.Application.DTO;
using Person.Application.Queries.ListPeople;
using Person.Application.Services.Outbox;
using Person.Domain.Entities;

namespace Person.Application.Maps
{
    public class PersonAppProfile : Profile
    {
        public PersonAppProfile()
        {
            CreateMap<DomainEntities.Person, ListPeopleDTO>();
            CreateMap<DomainEntities.Person, PersonDTO>(); 
            CreateMap<PersonDTO, DomainEntities.Person>();

            CreateMap<PersonDTO, OutBoxDTO>()
                .ForMember(dest => dest.Data, opt => opt.MapFrom(src =>src))
                .ForMember(dest => dest.DataType, opt => opt.MapFrom(src => src.IndexName()));

            CreateMap<OutBoxDTO, Outbox>()
                .ForMember(dest => dest.Data, opt => opt.MapFrom(src => JsonConvert.SerializeObject(src.Data)))
                .ForMember(dest => dest.DataID, opt => opt.MapFrom(src => src.ID))
                .ForMember(dest => dest.CreationDate, opt => opt.MapFrom(src => DateTime.Now));

        }
    }
}
