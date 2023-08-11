using Blog.Api.DTO;
using Blog.Api.Extensions;
using Blog.Application.UseCases.Commands;
using Blog.Application.UseCases.DTO.Searches;
using Blog.Application.UseCases.Queries;
using Blog.Implementation;
using FluentValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Blog.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]

    public class UsersController : ControllerBase
    {
        public static IEnumerable<string> AllowedExtensions => new List<string> { ".jpg", ".png", ".jpeg", ".gif",".JPG",".JPEG",".PNG",".GIF"};

        private UseCaseHandler _handler;
   



        public UsersController(UseCaseHandler handler)
        {
            _handler = handler;
        }

        /// <summary>
        /// Get users.
        /// </summary>
        /// <param name="search"></param> 
        /// <param name="query"></param>
        /// <returns>All users</returns>
        /// 
        /// <response code="200">Users are returned.</response>
        /// <response code="401">Unauthorized.</response>
        /// <response code="500">Unexpected server error.</response>
        /// 

        // GET api/<UsersController>
        [HttpGet]
        public IActionResult Get([FromQuery] UsersPagedSearch search, [FromServices] IGetUsersQuery query)
        {
            return Ok(_handler.HandleQuery(query, search));
        }


        /// <summary>
        /// Get one user.
        /// </summary>
        /// <param name="id"></param> 
        /// <param name="query"></param>
        /// <returns>One User</returns>
        /// 
        /// <response code="200">User is found.</response>
        /// <response code="401">Unauthorized.</response>
        /// <response code="404">User is not found.</response>
        /// <response code="500">Unexpected server error.</response>
        /// 

        // GET api/<UsersController>/5

        [HttpGet("{id}")]
        public IActionResult Get(int id, [FromServices] IGetUserQuery query)
        {
            return Ok(_handler.HandleQuery(query, id));
        }


        /// <summary>
        /// User Registration
        /// </summary>
        /// <param name="dto"></param> 
        /// <param name="command"></param>
        /// <returns>A newly created user</returns>
        /// <response code="201">Successfull creation.</response> 
        /// <response code="401">Unauthorized.</response>
        /// <response code="422">Validation failure.</response>
        /// <response code="500">Unexpected server error.</response>

        // POST api/<UsersController>
        [AllowAnonymous]
        [HttpPost]
        public IActionResult Post([FromForm] RegisterImageApiDto dto, [FromServices] IRegisterUserCommand command)
        {
            if (dto.Image != null)
            {
                var guid = Guid.NewGuid();
                var extension = Path.GetExtension(dto.Image.FileName);

                if (!AllowedExtensions.Contains(extension))
                {
                    throw new InvalidOperationException("Unsupported file type.");
                }

                var fileName = guid + extension;

                var filePath = Path.Combine("wwwroot", "images", fileName);

                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    dto.Image.CopyTo(fileStream);
                };

                dto.ImageFileName = fileName;
     
            }
            _handler.HandleCommand(command, dto);
            return StatusCode(201);
        }


        /// <summary>
        /// User deactivation
        /// </summary>
        /// <param name="id"></param> 
        /// <param name="command"></param>
        /// <returns>User is deactivated</returns>
        /// 
        /// <response code="204">No content.</response>
        /// <response code="401">Unauthorized.</response>
        /// <response code="404">User to deactivate is not found.</response>
        /// <response code="500">Unexpected server error.</response>
        /// 

        // PATCH api/<UsersController>/5
        [HttpPatch("{id}")]
        public IActionResult Patch(int id, [FromServices] IUserActivationCommand command)
        {
            _handler.HandleCommand(command, id);
            return NoContent();
        }


        /// <summary>
        /// User delete
        /// </summary>
        /// <param name="id"></param> 
        /// <param name="command"></param>
        /// <returns>User is deleted</returns>
        /// 
        /// <response code="204">No content.</response>
        /// <response code="401">Unauthorized.</response>
        /// <response code="404">User to delete is not found.</response>
        /// <response code="500">Unexpected server error.</response>
        /// 
        /// 
        /// 
        // DELETE api/<UsersController>/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id, [FromServices] IDeleteUserUsingIntCommand command)
        {
            _handler.HandleCommand(command, id);
            return NoContent();
        }


        // PATCH api/<UsersController>/5
        [HttpPatch("deactivate/{id}")]
        public IActionResult Patch(int id, [FromServices] ISoftDeleteUserUsingIntCommand command)
        {
            _handler.HandleCommand(command, id);
            return NoContent();
        }


        [HttpPatch("edit")]
        public IActionResult Patch([FromForm] EditUserDTO dto, [FromServices] IEditUserCommand command)
        {
            if (dto.Image != null)
            {
                var guid = Guid.NewGuid();
                var extension = Path.GetExtension(dto.Image.FileName);

                if (!AllowedExtensions.Contains(extension))
                {
                    throw new InvalidOperationException("Unsupported file type.");
                }

                var fileName = guid + extension;

                var filePath = Path.Combine("wwwroot", "images", fileName);

                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    dto.Image.CopyTo(fileStream);
                };

                dto.ImageFileName = fileName;

            }
            _handler.HandleCommand(command, dto);
            return NoContent();
        }




    }
}
