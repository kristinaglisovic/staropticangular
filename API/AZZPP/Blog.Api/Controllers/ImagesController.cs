using Blog.Api.DTO;
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

    public class ImagesController : ControllerBase
    {
        private UseCaseHandler _handler;
        public ImagesController(UseCaseHandler handler)
        {
            _handler = handler;
        }

        /// <summary>
        /// Get all images with pagination.
        /// </summary>
        /// <param name="search"></param> 
        /// <param name="query"></param>
        /// <returns>All images</returns>
        /// 
        /// <response code="200">Images are returned.</response>
        /// <response code="401">Unauthorized.</response>
        /// <response code="500">Unexpected server error.</response>
        /// 


        // GET: api/<ImagesController>
        [HttpGet]
        public IActionResult Get([FromQuery] ImagePagedSearch search, [FromServices] IGetImagesQuery query)
        {
            return Ok(_handler.HandleQuery(query, search));
        }


        /// <summary>
        /// Get one image.
        /// </summary>
        /// <param name="id"></param> 
        /// <param name="query"></param>
        /// <returns>One image</returns>
        /// 
        /// <response code="200">Image is found.</response>
        /// <response code="401">Unauthorized.</response>
        /// <response code="404">Image is not found.</response>
        /// <response code="500">Unexpected server error.</response>

        // GET api/<ImagesController>/5
        [HttpGet("{id}")]
        public IActionResult Get(int id, [FromServices] IGetImageQuery query)
        {
            return Ok(_handler.HandleQuery(query, id));
        }



        public static IEnumerable<string> AllowedExtensions => new List<string> { ".jpg", ".png", ".jpeg", ".gif", ".JPG", ".JPEG", ".PNG", ".GIF" };

        /// <summary>
        /// Creates new image.
        /// </summary>
        /// <param name="dto"></param> 
        /// <param name="command"></param>
        /// <returns>A newly created Image</returns>
        /// <response code="201">Successfull creation.</response> 
        /// <response code="401">Unauthorized.</response>
        /// <response code="500">Unexpected server error.</response>


        // POST api/<ImagesController>
        [HttpPost]
        public IActionResult Post([FromForm] ImageImageApiDto dto, [FromServices] ICreateImageCommand command)
        {
                if (dto.Image == null)
                {
                     throw new NullReferenceException("Image key is required");
                }
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

                dto.Path= fileName;

            _handler.HandleCommand(command, dto);
            return StatusCode(201);
        }


        /// <summary>
        /// Image deactivation
        /// </summary>
        /// <param name="id"></param> 
        /// <param name="command"></param>
        /// <returns>Image is deactivated</returns>
        /// 
        /// <response code="204">No content.</response>
        /// <response code="401">Unauthorized.</response>
        /// <response code="404">Image to deactivate is not found.</response>
        /// <response code="409">Conflict.</response>
        /// <response code="500">Unexpected server error.</response>
        /// 

        // PATCH api/<ImagesController>/5
        [HttpPatch("{id}")]
        public IActionResult Patch(int id, [FromServices] ISoftDeleteImageUsingIntCommand command)
        {
            _handler.HandleCommand(command, id);
            return NoContent();
        }


        /// <summary>
        /// Image delete
        /// </summary>
        /// <param name="id"></param> 
        /// <param name="command"></param>
        /// <returns>Image is deleted</returns>
        ///
        /// <response code="204">No content.</response>
        /// <response code="401">Unauthorized.</response>
        /// <response code="404">Image to delete is not found.</response>
        /// <response code="409">Conflict.</response>
        /// <response code="500">Unexpected server error.</response>
        /// 


        // DELETE api/<ImagesController>/5

        [HttpDelete("{id}")]
        public IActionResult Delete(int id, [FromServices] IDeleteImageUsingIntCommand command)
        {
            _handler.HandleCommand(command, id);
            return NoContent();
        }
    }
}
