 
    using Blog.Application.UseCases.Commands;
    using Blog.Application.UseCases.DTO.Like;
    using Blog.Application.UseCases.DTO.Post;
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
    public class PostsController : ControllerBase
    {
        private UseCaseHandler _handler;

        public PostsController(UseCaseHandler handler)
        {
            _handler = handler;
        }


        /// <summary>
        /// Get all posts with pagination.
        /// </summary>
        /// <param name="search"></param> 
        /// <param name="query"></param>
        /// <returns>All posts</returns>
        /// 
        /// <response code="200">Posts are returned.</response>
        /// <response code="500">Unexpected server error.</response>


        // GET: api/<PostsController>

        [AllowAnonymous]
        [HttpGet]
        public IActionResult Get([FromQuery] PostPagedSearch search,
            [FromServices] IGetPostsQuery query)
        {

            return Ok(_handler.HandleQuery(query, search));

        }

        /// <summary>
        /// Get one post.
        /// </summary>
        /// <param name="id"></param> 
        /// <param name="eng"></param> 
        /// <param name="query"></param>
        /// <returns>One post</returns>
        /// 
        /// <response code="200">Post is found.</response>
        /// <response code="404">Post is not found.</response>
        /// <response code="500">Unexpected server error.</response>


        [AllowAnonymous]

        // GET api/<PostsController>/5
        [HttpGet("{id}")]
        public IActionResult Get(int id, [FromQuery] bool? eng, [FromServices] IGetPostQuery query)
        {
            var search = new SingleTranslatedSearch(id, eng);

            return Ok(_handler.HandleQuery(query, search));
         
        }

 

        [HttpPatch("like")]

        public IActionResult Patch([FromBody] CreateLikeDTO dto,
                             [FromServices] IUpdatePostLikeCommand command)
        {
            _handler.HandleCommand(command, dto);
            return NoContent();
        }


        /// <summary>
        /// Creates new post.
        /// </summary>
        /// <param name="dto"></param> 
        /// <param name="command"></param>
        /// <returns>A newly created Post</returns>
        /// /// <remarks>
        /// Sample request:
        ///
        ///     POST / api/Posts
        ///     {
        ///        "title": "New Title",
        ///        "description": "New Desc",
        ///        "userId": 1,
        ///        "categoriesIds": [
        ///                  1,2   
        ///              ],
        ///        "tagsIds": [
        ///                  1
        ///              ],
        ///        "imagesIds": [
        ///                  2,4
        ///              ]
        ///     }
        /// </remarks> 
        /// <response code="201">Successfull creation.</response> 
        /// <response code="401">Unauthorized.</response>
        /// <response code="422">Validation failure.</response>
        /// <response code="500">Unexpected server error.</response>




        // POST api/<PostsController>
        [HttpPost]
        public IActionResult Post([FromBody] CreatePostDTO dto,
                                      [FromServices] ICreatePostCommand command)
        {
            _handler.HandleCommand(command, dto);
            return StatusCode(201);
        }


        /// <summary>
        /// Post deactivation
        /// </summary>
        /// <param name="id"></param> 
        /// <param name="command"></param>
        /// <returns>Post is deactivated</returns>
        /// 
        /// <response code="204">No content.</response>
        /// <response code="401">Unauthorized.</response>
        /// <response code="404">Post to deactivate is not found.</response>
        /// <response code="500">Unexpected server error.</response>

        // PATCH api/<PostsController>/5
        [HttpPatch("deactivate/{id}")]
        public IActionResult Patch(int id, [FromServices] ISoftDeletePostUsingIntCommand command)
        {
            _handler.HandleCommand(command, id);
            return NoContent();
        }




        // PATCH api/<PostsController>/5
        [HttpPatch("{id}")]
        public IActionResult Patch(int id, [FromServices] IPostActivationCommand command)
        {
            _handler.HandleCommand(command, id);
            return NoContent();
        }
        





        /// <summary>
        /// Delete a post
        /// </summary>
        /// <param name="id"></param> 
        /// <param name="command"></param>
        /// <returns>Post is deleted</returns>
        /// 
        /// <response code="204">No content.</response>
        /// <response code="401">Unauthorized.</response>
        /// <response code="404">Post to delete is not found.</response>
        /// <response code="500">Unexpected server error.</response>

        // DELETE api/<PostsController>/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id, [FromServices] IDeletePostUsingIntCommand command)
        {
            _handler.HandleCommand(command, id);
            return NoContent();
        }


        [HttpPatch("edit")]
        public IActionResult Patch([FromBody] EditPostDTO dto, [FromServices] IEditPostCommand command)
        { 
            _handler.HandleCommand(command, dto);
            return NoContent();
        }
    }
}
