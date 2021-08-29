 
using BookManagementModels.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnitOfWork;
using System.Linq;
using Business.Book.DTO;
using Business.Book.DTO.Maps;
using AutoMapper;
using Indexer;

namespace Business.Book
{
    /// <summary>
    /// Functions for book management
    /// </summary>
    public class BookBusiness : BaseBusiness, IBookBusiness
    {


        public BookBusiness(IUnitOfWork uow, IMapper mapper, IIndexer indexer) :base(uow,mapper, indexer)
        {         
        }

        /// <summary>
        /// Save Book Info
        /// </summary>
        /// <param name="book">book to save</param>
        /// <returns></returns>
        public async Task<BooksDTO> SaveBook(BooksDTO book)
        {
           var result= await Upsert<BooksDTO,Books>(book);
            indexer.Index<BooksDTO>(book.IndexName, result.ID.ToString(), result);
            return result;
        }

   
        /// <summary>
        /// Get Book by ID
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public  BooksDTO GetBook(Guid id)
        { 
           return Mapper.Map<BooksDTO>(Uow.CreateRepository<Books>().GetById(id));
        }


        public  void ReIndexBooks()
        {
            var rs= Uow.CreateRepository<Books>().List();
            foreach (var item in rs)
            {
                var sc= Mapper.Map<BooksDTO>(item);
                indexer.Index<BooksDTO>(sc.IndexName, sc.ID.ToString(),sc);
            }
             
        }
         
    }
}
