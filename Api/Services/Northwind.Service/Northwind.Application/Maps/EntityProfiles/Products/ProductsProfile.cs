//using AutoMapper;
//using Newtonsoft.Json;
//using Northwind.Application.Models.DTO;
//using Northwind.Domain.Entities;

//namespace Northwind.Application.Maps.EntityProfiles.Products
//{
//    public partial class NorthwindMapProfile : Profile
//    {
//        public NorthwindMapProfile()
//        {


//            CreateMap<ProductsDTO, Product>();
//            CreateMap<Product, ProductsDTO>()
//                .ForMember(dest => dest.CategoryName, opt => opt.MapFrom(src => src.Category == null ? string.Empty : src.Category.CategoryName))
//                .ForMember(dest => dest.SupplierName, opt => opt.MapFrom(src => src.Supplier == null ? string.Empty : src.Supplier.CompanyName));


//        }
//    }
//}
