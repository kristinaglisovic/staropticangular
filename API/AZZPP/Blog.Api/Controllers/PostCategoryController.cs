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
    public class PostCategoryController : ControllerBase
    {
        private UseCaseHandler _handler;

        public PostCategoryController(UseCaseHandler handler)
        {
            _handler = handler;
        }

        /// <summary>
        /// Get all posts categories with pagination.
        /// </summary>
        /// <param name="search"></param> 
        /// <param name="query"></param>
        /// <returns>All posts with categories</returns>
        /// 
        /// <response code="200">Posts with categories are returned.</response>
        /// <response code="500">Unexpected server error.</response>
        /// 


        // GET: api/<PostCategoryController>
      //  [AllowAnonymous]
        [HttpGet]
        public IActionResult Get([FromQuery] PostCategoryPagedSearch search,
            [FromServices] IGetPostCategoryQuery query)
        {

            return Ok(_handler.HandleQuery(query, search));

        }

    }
}
