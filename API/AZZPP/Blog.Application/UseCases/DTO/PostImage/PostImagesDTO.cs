using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog.Application.UseCases.DTO.PostImage
{
    public class PostImagesDTO
    {
        public int PostId { get; set; }
        public string PostName { get; set; }
        public string ImagePath { get; set; }
        public int ImageId { get; set; }
    }
}
