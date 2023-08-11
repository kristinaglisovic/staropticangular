using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog.Application.UseCases.DTO.Image
{
    public class ImageDTO :BaseDTO
    {
        public string Path { get; set; }
        public DateTime CreatedAt { get; set; }
        public string User { get; set; }
        public IEnumerable<PostDto> Posts { get; set; }
    }

  
    public class PostDto : BaseDTO
    {
        public string Title { get; set; }
    }
}
