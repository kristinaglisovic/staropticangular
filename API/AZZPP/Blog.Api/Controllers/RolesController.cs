using Blog.Application.UseCases.Commands;
using Blog.Application.UseCases.DTO.Roles;
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
    public class RolesController : ControllerBase
    {

        private UseCaseHandler _handler;
        public RolesController(UseCaseHandler handler)
        {
            _handler = handler;
        }


        /// <summary>
        /// Get roles.
        /// </summary>
        /// <param name="search"></param> 
        /// <param name="query"></param>
        /// <returns>All roles</returns>
        /// 
        /// <response code="200">Roles are returned.</response>
        /// <response code="401">Unauthorized.</response>
        /// <response code="500">Unexpected server error.</response>
        /// 

        // GET: api/<RolesController>
        [HttpGet]
        public IActionResult Get([FromQuery] BaseSearch search, [FromServices] IGetRolesQuery query)
        {
            return Ok(_handler.HandleQuery(query, search));
        }

        /// <summary>
        /// Get one role.
        /// </summary>
        /// <param name="id"></param> 
        /// <param name="query"></param>
        /// <returns>One role</returns>
        /// 
        /// <response code="200">Role is found.</response>
        /// <response code="401">Unauthorized.</response>
        /// <response code="404">Role is not found.</response>
        /// <response code="500">Unexpected server error.</response>


        // GET api/<RolesController>/5
        [HttpGet("{id}")]
        public IActionResult Get(int id, [FromServices] IGetRoleQuery query)
        {
            return Ok(_handler.HandleQuery(query, id));
        }

        /// <summary>
        /// Creates new role.
        /// </summary>
        /// <param name="dto"></param> 
        /// <param name="command"></param>
        /// <returns>A newly created role</returns>
        /// /// <remarks>
        /// Sample request:
        ///
        ///     POST / api/Roles
        ///     {
        ///        "name": "New Role       
        ///     }
        /// </remarks> 
        /// <response code="201">Successfull creation.</response> 
        /// <response code="401">Unauthorized.</response>
        /// <response code="422">Validation failure.</response>
        /// <response code="500">Unexpected server error.</response>

        // POST api/<RolesController>
        [HttpPost]
        public IActionResult Post([FromBody] CreateRoleDTO dto,
                         [FromServices] ICreateRoleCommand command)
        {
            _handler.HandleCommand(command, dto);
            return StatusCode(201);
        }

        /// <summary>
        /// Role deactivation
        /// </summary>
        /// <param name="id"></param> 
        /// <param name="command"></param>
        /// <returns>Role is deactivated</returns>
        /// 
        /// <response code="204">No content.</response>
        /// <response code="401">Unauthorized.</response>
        /// <response code="409">Conflict.</response>
        /// <response code="404">Role to deactivate is not found.</response>
        /// <response code="500">Unexpected server error.</response>
        /// 

        // PATCH api/<RolesController>/5
        [HttpPatch("{id}")]
        public IActionResult Patch(int id, [FromServices] ISoftDeleteRoleUsingIntCommand command)
        {
            _handler.HandleCommand(command, id);
            return NoContent();
        }

        /// <summary>
        /// Role delete
        /// </summary>
        /// <param name="id"></param> 
        /// <param name="command"></param>
        /// <returns>Role is deleted</returns>
        /// 
        /// <response code="204">No content.</response>
        /// <response code="401">Unauthorized.</response>
        /// <response code="409">Conflict.</response>
        /// <response code="404">Role to delete is not found.</response>
        /// <response code="500">Unexpected server error.</response>
        /// 


        // DELETE api/<RolesController>/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id, [FromServices] IDeleteRoleUsingIntCommand command)
        {
            _handler.HandleCommand(command, id);
            return NoContent();
        }
    }
}
