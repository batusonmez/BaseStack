
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
        void ReIndexBooks();
    }
}
