using Business.Comment;
using Business.Comment.DTO;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Api.Comment.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CommentController : ControllerBase
    {
        private static readonly string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        private readonly ILogger<CommentController> _logger;
        private readonly ICommentBusiness business;

        public CommentController(ILogger<CommentController> logger, ICommentBusiness business)
        {
            _logger = logger;
            this.business = business;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult Get(string contextID)
        {
            if (string.IsNullOrEmpty(contextID))
            {
                return BadRequest();
            }
            return Ok(business.Get(contextID));
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult Post(CommentDTO comment)
        {
            if ( comment==null || string.IsNullOrEmpty(comment.ContextID))
            {
                return BadRequest();
            }
            business.SaveComment(comment);
            return Ok();
        }

    }
}
