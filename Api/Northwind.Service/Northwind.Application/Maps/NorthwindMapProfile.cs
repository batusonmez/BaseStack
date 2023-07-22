using AutoMapper;
using Newtonsoft.Json;
using Northwind.Application.Models.DTO;
using Northwind.Domain.Entities;

namespace Northwind.Application.Maps
{
    public partial class NorthwindMapProfile : Profile
    {
        public NorthwindMapProfile()
        {
                       

            CreateMap<ProductsDTO, Product>();
            CreateMap<Product, ProductsDTO>()
                .ForMember(dest => dest.CategoryName, opt => opt.MapFrom(src => src.Category == null ? string.Empty : src.Category.CategoryName))
                .ForMember(dest => dest.SupplierName, opt => opt.MapFrom(src => src.Supplier == null ? string.Empty : src.Supplier.CompanyName));

            CreateMap<OutBoxDTO, Outbox>() 
                .ForMember(dest => dest.Data, opt => opt.MapFrom(src => JsonConvert.SerializeObject(src.Data)))
                .ForMember(dest => dest.CreationDate, opt => opt.MapFrom(src => DateTime.Now));

            CreateMap<CustomersDTO, Customer>();
            CreateMap<Customer, CustomersDTO>();

            CreateMap<CategoryDTO, Category>();
            CreateMap<Category, CategoryDTO>();

            CreateMap<CategoryDTO, OutBoxDTO>()
            .ForMember(dest => dest.Data, opt => opt.MapFrom(src => src))
            .ForMember(dest => dest.DataID, opt => opt.MapFrom(src => src.CategoryId.ToString()))
            .ForMember(dest => dest.DataType, opt => opt.MapFrom(src => src.IndexKey));

             


        }
    }
}
