 
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
    public class BookBusiness : BaseController, IBookBusiness
    {
        private readonly IIndexer indexer;

        public BookBusiness(IUnitOfWork uow, IMapper mapper, IIndexer indexer) :base(uow,mapper)
        {
            this.indexer = indexer;
        }

        /// <summary>
        /// Save Book Info
        /// </summary>
        /// <param name="book">book to save</param>
        /// <returns></returns>
        public async Task<BooksDTO> SaveBook(BooksDTO book)
        {
           var result= await Upsert<BooksDTO,Books>(book);
            indexer.Index<BooksDTO>(BooksDTO.IndexName, result.ID.ToString(), result);
            return result;
        }


        public void Search(string Context)
        {
             

        }

    }
}
