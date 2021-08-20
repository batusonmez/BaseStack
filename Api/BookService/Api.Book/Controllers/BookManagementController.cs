
using Business.Book;
using Business.Book.DTO;
using Indexer;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net.Mime;
using System.Threading.Tasks;

namespace Api.BookManagement.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookManagementController : ControllerBase
    {
        private readonly IBookBusiness business;
        private readonly IIndexer indexer;

        public BookManagementController(IBookBusiness business, IIndexer indexer)
        {
            this.business = business;
            this.indexer = indexer;
        }

        [HttpPost]
        [Consumes(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Post(BooksDTO book)
        {
            if (book == null)
            {
                return BadRequest();
            }             
            await business.SaveBook(book);
            var resp=indexer.Index<BooksDTO>(BooksDTO.IndexName,book.ID.ToString(),book);
            return Ok();
        }


        [HttpPost]
        [Route("search")]
        [Consumes(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public IActionResult Search(string term)
        {
            return Ok(indexer.Search<BooksDTO>(BooksDTO.IndexName,term));
        }

    }
}
