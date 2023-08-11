using Blog.Application.UseCases.DTO.Image;
using Blog.Application.UseCases.DTO.User;
using Microsoft.AspNetCore.Http;

namespace Blog.Api.DTO
{
    public class RegisterImageApiDto : RegisterDTO
    {
        public IFormFile Image { get; set; }
    }
    public class ImageImageApiDto :ImageDTO
    {
        public IFormFile Image { get; set; }
    }

    public class EditUserDTO:UserEditDTO
    {   
        public IFormFile Image { get; set; }
    }
}
