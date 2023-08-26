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

            CreateMap<OutBoxDTO, Outbox>()
                .ForMember(dest => dest.Data, opt => opt.MapFrom(src => JsonConvert.SerializeObject(src.Data)))
                .ForMember(dest => dest.CreationDate, opt => opt.MapFrom(src => DateTime.Now));

            CreateMap<ProductsDTO, Product>()
                .ForMember(dest => dest.IndexTrace, opt => opt.MapFrom((src,desc) => desc.IndexTrace.HasValue?desc.IndexTrace:src.IndexTrace));
            CreateMap<Product, ProductsDTO>()
                .ForMember(dest => dest.CategoryName, opt => opt.MapFrom(src => src.Category == null ? string.Empty : src.Category.CategoryName))
                .ForMember(dest => dest.SupplierName, opt => opt.MapFrom(src => src.Supplier == null ? string.Empty : src.Supplier.CompanyName));
            CreateMap<ProductsDTO, OutBoxDTO>()
                .ForMember(dest => dest.Data, opt => opt.MapFrom(src => src))
                .ForMember(dest => dest.DataID, opt => opt.MapFrom(src => src.IndexTrace.ToString()))
                .ForMember(dest => dest.DataType, opt => opt.MapFrom(src => src.IndexKey));


            CreateMap<CustomersDTO, Customer>();
            CreateMap<Customer, CustomersDTO>();

            CreateMap<CategoryDTO, Category>();
            CreateMap<Category, CategoryDTO>();
            CreateMap<CategoryDTO, OutBoxDTO>()
            .ForMember(dest => dest.Data, opt => opt.MapFrom(src => src))
            .ForMember(dest => dest.DataID, opt => opt.MapFrom(src => src.CategoryId.ToString()))
            .ForMember(dest => dest.DataType, opt => opt.MapFrom(src => src.IndexKey));

        
            CreateMap<SupplierDTO, Supplier>();
            CreateMap<Supplier, SupplierDTO>();
            CreateMap<SupplierDTO, OutBoxDTO>()
            .ForMember(dest => dest.Data, opt => opt.MapFrom(src => src))
            .ForMember(dest => dest.DataID, opt => opt.MapFrom(src => src.SupplierId.ToString()))
            .ForMember(dest => dest.DataType, opt => opt.MapFrom(src => src.IndexKey));

        }
    }
}
