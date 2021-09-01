
using Business.Book.DTO;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Business.Book
{
    /// <summary>
    /// Functions for book management
    /// </summary>
    public interface IBookBusiness : IBusiness
    {
        /// <summary>
        /// Save Book Info
        /// </summary>
        /// <param name="book">book to save</param>
        /// <returns></returns>
        Task<BooksDTO> SaveBook(BooksDTO book);

        /// <summary>
        /// Get book by ID
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        BooksDTO GetBook(Guid id);

        /// <summary>
        /// Delete Book by ID
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<int> Delete(params object[] id);
        void ReIndexBooks();
    }
}
