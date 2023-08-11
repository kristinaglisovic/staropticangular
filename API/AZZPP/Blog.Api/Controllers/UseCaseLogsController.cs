using Blog.Application.UseCases;
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
    public class UseCaseLogsController : ControllerBase
    {
        private UseCaseHandler _handler;

        public UseCaseLogsController(UseCaseHandler handler)
        {
            _handler = handler;
        }

        /// <summary>
        /// Get use case logs.
        /// </summary>
        /// <param name="search"></param> 
        /// <param name="query"></param>
        /// <returns>All use case logs</returns>
        /// 
        /// <response code="200">Use case logs are returned.</response>
        /// <response code="401">Unauthorized.</response>
        /// <response code="500">Unexpected server error.</response>
        /// 



        // GET: api/<UseCaseLogsController>

        [HttpGet]
        public IActionResult Get(
            [FromQuery] UseCaseLogSearch search,
            [FromServices] IGetUseCaseLogsQuery query)
        {
            return Ok(_handler.HandleQuery(query, search));
        }
    }
}