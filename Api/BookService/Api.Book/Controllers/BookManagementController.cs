using BookManagementModels.DTO;
using Business.Book;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
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
            return Ok();
        }
    }
}
