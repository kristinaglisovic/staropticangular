using Blog.Api.Core;
using Blog.Application.UseCases.DTO.User;
using Blog.DataAccess;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;

namespace Blog.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class TokenController : ControllerBase
    {
        private readonly JwtManager _manager;

        public TokenController(JwtManager manager)
        {
            _manager = manager;
        }


        /// <summary>
        /// Creates new token.
        /// </summary>
        /// <param name="request"></param> 
        /// <returns>A newly created user token</returns>
        /// /// <remarks>
        /// Sample request:
        ///
        ///     POST / api/Token
        ///     {
        ///        "email": "email"
        ///        "password":"password"
        ///     }
        /// </remarks> 
        /// <response code="201">Successfull creation.</response> 
        /// <response code="401">Unauthorized.</response>
        /// <response code="500">Unexpected server error.</response>
        /// 

        [HttpPost]
        [AllowAnonymous]
        public IActionResult Post([FromBody] TokenRequest request)
        {
            try
            {
                var token = _manager.MakeToken(request.Email,request.Password);

               
                return Ok(new { token});


            }
            catch (UnauthorizedAccessException)
            {
                return Unauthorized();
            }
            catch (System.Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }
    }

    public class TokenRequest
    {
        public string Email { get; set; }
        public string Password { get; set; }
    }
}
