using BookManagementModels.DTO;
using BookManagementModels.DTO.Maps;
using BookManagementModels.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnitOfWork;
using System.Linq;
namespace Business.Book
{
    /// <summary>
    /// Functions for book management
    /// </summary>
    public class BookBusiness : IBookBusiness
    {
        private readonly IUnitOfWork uow;

        public BookBusiness(IUnitOfWork uow)
        {
            this.uow = uow;
        }

        /// <summary>
        /// Save Book Info
        /// </summary>
        /// <param name="book">book to save</param>
        /// <returns></returns>
        public Task<int> SaveBook(BooksDTO book)
        {
            var bookRepo = uow.CreateRepository<Books>();
            if (book.HasID)
            {
                var entity = bookRepo.GetById(book.ID);
                entity.Title = book.Title;
                entity.Description = book.Description;
                //etc...

                bookRepo.Update(entity);
            }
            else
            {

                bookRepo.Insert(book.Map());
            }

            return uow.Commit();
        }


        public void Search(string Context)
        {
             


        }

    }
}
