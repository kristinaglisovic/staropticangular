using Blog.Application.UseCases.Commands;
using Blog.Application.UseCases.DTO.Like;
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
    public class LikesController : ControllerBase
    {

        private UseCaseHandler _handler;

        public LikesController(UseCaseHandler handler)
        {
            _handler = handler;
        }

        /// <summary>
        /// Get all likes with pagination.
        /// </summary>
        /// <param name="search"></param> 
        /// <param name="query"></param>
        /// <returns>All likes</returns>
        /// 
        /// <response code="200">Likes are returned.</response>
        /// <response code="401">Unauthorized.</response>
        /// <response code="500">Unexpected server error.</response>
        /// 


        // GET: api/<LikesController>
        [HttpGet]
        public IActionResult Get([FromQuery] LikePagedSearch search, [FromServices] IGetLikesQuery query)
        {
            return Ok(_handler.HandleQuery(query, search));
        }


        /// <summary>
        /// Get one like.
        /// </summary>
        /// <param name="id"></param> 
        /// <param name="query"></param>
        /// <returns>One like</returns>
        /// 
        /// <response code="200">Like is found.</response>
        /// <response code="401">Unauthorized.</response>
        /// <response code="404">Like is not found.</response>
        /// <response code="500">Unexpected server error.</response>



        // GET api/<LikesController>/5
        [HttpGet("{id}")]
        public IActionResult Get(int id, [FromServices] IGetLikeQuery query)
        {
            return Ok(_handler.HandleQuery(query, id));
        }


        /// <summary>
        /// Creates new like.
        /// </summary>
        /// <param name="dto"></param> 
        /// <param name="command"></param>
        /// <returns>A newly created Like</returns>
        /// /// <remarks>
        /// Sample request:
        ///
        ///     POST / api/Likes
        ///     {
        ///          "userId": 1,
        ///          "postId": 5,
        ///     }
        /// </remarks> 
        /// <response code="201">Successfull creation.</response> 
        /// <response code="401">Unauthorized.</response>
        /// <response code="409">Conflict.</response>
        /// <response code="422">Validation failure.</response>
        /// <response code="500">Unexpected server error.</response>
        /// 



        // POST api/<LikesController>
        [HttpPost]
        public IActionResult Post([FromBody] CreateLikeDTO dto,
                             [FromServices] ICreateLikeCommand command)
        {
            _handler.HandleCommand(command, dto);
            return StatusCode(201);
        }


        /// <summary>
        /// Like deactivation
        /// </summary>
        /// <param name="id"></param> 
        /// <param name="command"></param>
        /// <returns>Like is deactivated</returns>
        /// 
        /// <response code="204">No content.</response>
        /// <response code="401">Unauthorized.</response>
        /// <response code="404">Like to deactivate is not found.</response>
        /// <response code="500">Unexpected server error.</response>


        // PATCH api/<LikesController>/5
        [HttpPatch("{id}")]
        public IActionResult Patch(int id, [FromServices] ISoftDeleteLikeUsingIntCommand command)
        {
            _handler.HandleCommand(command, id);
            return NoContent();
        }
        /// <summary>
        /// Like delete
        /// </summary>
        /// <param name="id"></param> 
        /// <param name="command"></param>
        /// <returns>Like is deleted</returns>
        /// 
        /// <response code="204">No content.</response>
        /// <response code="401">Unauthorized.</response>
        /// <response code="404">Like to delete is not found.</response>
        /// <response code="500">Unexpected server error.</response>

        // DELETE api/<LikesController>/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id, [FromServices] IDeleteLikeUsingIntCommand command)
        {
            _handler.HandleCommand(command, id);
            return NoContent();
        }
    }
}
