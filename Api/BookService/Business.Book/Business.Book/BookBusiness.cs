 
using BookManagementModels.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnitOfWork;
using System.Linq;
using Business.Book.DTO;
using Business.Book.DTO.Maps;
using AutoMapper;

namespace Business.Book
{
    /// <summary>
    /// Functions for book management
    /// </summary>
    public class BookBusiness : BaseController, IBookBusiness
    {
        
        public BookBusiness(IUnitOfWork uow, IMapper mapper) :base(uow,mapper)
        {
            
        }

        /// <summary>
        /// Save Book Info
        /// </summary>
        /// <param name="book">book to save</param>
        /// <returns></returns>
        public async Task<BooksDTO> SaveBook(BooksDTO book)
        {
           return await Upsert<BooksDTO,Books>(book);
             
        }


        public void Search(string Context)
        {
             

        }

    }
}
