//using AutoMapper;
//using Newtonsoft.Json;
//using Northwind.Application.Models.DTO;
//using Northwind.Domain.Entities;

//namespace Northwind.Application.Maps.EntityProfiles.MsgOutbox
//{
//    public partial class NorthwindMapProfile : Profile
//    {
//        public NorthwindMapProfile()
//        {

//            CreateMap<OutBoxDTO, Outbox>()
//                       .ForMember(dest => dest.Data, opt => opt.MapFrom(src => JsonConvert.SerializeObject(src.Data)))
//                       .ForMember(dest => dest.CreationDate, opt => opt.MapFrom(src => DateTime.Now));


//        }
//    }
//}
