using Blog.Application.UseCases.Commands;
using Blog.Application.UseCases.DTO.User;
using Blog.Domain.Entities;
using Blog.Implementation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Blog.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class UserUseCasesController : ControllerBase
    {

        private UseCaseHandler _handler;

        public UserUseCasesController(UseCaseHandler handler)
        {
            _handler = handler;
        }

        /// <summary>
        /// Update user use cases
        /// </summary>
        /// <param name="request"></param> 
        /// <param name="command"></param>
        /// <returns>Updated user cases</returns>
        /// 
        /// <response code="204">No content.</response>
        /// <response code="401">Unauthorized.</response>
        /// <response code="500">Unexpected server error.</response>
        /// 

        [HttpPut]
        public IActionResult Put([FromBody] UpdateUserUseCasesDTO request, [FromServices] IUpdateUserUseCasesCommand command)
        {
           
            _handler.HandleCommand(command,request);
            return NoContent();

        }

    }
}
