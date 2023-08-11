using Blog.Application.UseCases.Commands;
using Blog.Application.UseCases.DTO.Comment;
using Blog.Application.UseCases.DTO.Searches;
using Blog.Application.UseCases.Queries;
using Blog.Implementation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Blog.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class CommentsController : ControllerBase
    {
        private UseCaseHandler _handler;

        public CommentsController(UseCaseHandler handler)
        {
            _handler = handler;
        }

        /// <summary>
        /// Get all comments with pagination.
        /// </summary>
        /// <param name="search"></param> 
        /// <param name="query"></param>
        /// <returns>All comments </returns>
        /// 
        /// <response code="200">Comments are returned.</response>
        /// <response code="500">Unexpected server error.</response>
        /// 

        // GET: api/<CommentsController>
       // [AllowAnonymous]
        [HttpGet]
        public IActionResult Get([FromQuery] CommentsPagedSearch search, [FromServices] IGetCommentsQuery query)
        {
            return Ok(_handler.HandleQuery(query, search));
        }

        /// <summary>
        /// Get one comment.
        /// </summary>
        /// <param name="id"></param> 
        /// <param name="query"></param>
        /// <returns>One comment</returns>
        /// 
        /// <response code="200">Comment is found.</response>
        /// <response code="404">Comment is not found.</response>
        /// <response code="500">Unexpected server error.</response>


        // GET api/<CommentsController>/5
      //  [AllowAnonymous]
        [HttpGet("{id}")]
        public IActionResult Get(int id, [FromServices] IGetCommentQuery query)
        {
            return Ok(_handler.HandleQuery(query, id));
        }

        /// <summary>
        /// Creates new comment.
        /// </summary>
        /// <param name="dto"></param> 
        /// <param name="command"></param>
        /// <returns>A newly created Comment</returns>
        /// /// <remarks>
        /// Sample request:
        ///
        ///     POST / api/Comments
        ///     {
        ///          "userId": 1,
        ///          "postId": 5,
        ///          "text": "string"
        ///     }
        /// </remarks> 
        /// <response code="201">Successfull creation.</response> 
        /// <response code="401">Unauthorized.</response>
        /// <response code="409">Conflict.</response>
        /// <response code="422">Validation failure.</response>
        /// <response code="500">Unexpected server error.</response>
        /// 

        // POST api/<CommentsController>
        [HttpPost]
        public IActionResult Post([FromBody] CreateCommentDTO dto,
                             [FromServices] ICreateCommentCommand command)
        {
            _handler.HandleCommand(command, dto);
            return StatusCode(201);
        }


        /// <summary>
        /// Comment deactivation
        /// </summary>
        /// <param name="id"></param> 
        /// <param name="command"></param>
        /// <returns>Comment is deactivated</returns>
        /// 
        /// <response code="204">No content.</response>
        /// <response code="401">Unauthorized.</response>
        /// <response code="404">Comment to deactivate is not found.</response>
        /// <response code="500">Unexpected server error.</response>



        // PATCH api/<CommentsController>/5
        [HttpPatch("{id}")]
        public IActionResult Patch(int id, [FromServices] ICommentActivationCommand command)
        {
            _handler.HandleCommand(command, id);
            return NoContent();
        }


    
        /// <summary>
        /// Comment delete
        /// </summary>
        /// <param name="id"></param> 
        /// <param name="command"></param>
        /// <returns>Comment is deleted</returns>
        /// 
        /// <response code="204">No content.</response>
        /// <response code="401">Unauthorized.</response>
        /// <response code="404">Comment to delete is not found.</response>
        /// <response code="500">Unexpected server error.</response>



        // DELETE api/<CommentsController>/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id, [FromServices] IDeleteCommentUsingIntCommand command)
        {
            _handler.HandleCommand(command, id);
            return NoContent();
        }


        
    }
}
