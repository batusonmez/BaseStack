
using Business.Book;
using Business.Book.DTO;
using Indexer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Net.Mime;
using System.Threading.Tasks;

namespace Api.BookManagement.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookManagementController : ControllerBase
    {
        private readonly IBookBusiness business;
        public BookManagementController(IBookBusiness business)
        {
            this.business = business;
        }

        [HttpPost]
        //  [Authorize]
        [Consumes(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Post([FromBody] BooksDTO book)
        {
            if (book == null)
            {
                return BadRequest();
            }
            await business.SaveBook(book);

            return Ok(book);
        }


        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult Get(Guid id)
        {
            var book = business.GetBook(id);
            if (book == null)
            {
                return NotFound();
            }
            return Ok(book);
        }


        [HttpDelete]
        [ProducesResponseType(StatusCodes.Status200OK)]        
        public async Task<IActionResult> Delete(Guid id)
        {
            await business.Delete(id);            
            return Ok();
        }

        [HttpPost]
        [Route("search")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public IActionResult Search([FromBody] SearchQueryDTO query)
        {
            return Ok(business.Search<BooksDTO>(query.Query));
        }


        [HttpGet]
        [Route("reindex")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public IActionResult reIndex()
        {
            business.ReIndexBooks();
            return Ok();
        }

    }
}
