using AutoMapper;
using BookManagementModels.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Book.DTO.Maps
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        { 
            CreateMap<Books, BooksDTO>();
            CreateMap<BooksDTO, Books>();
        }
    }
}
