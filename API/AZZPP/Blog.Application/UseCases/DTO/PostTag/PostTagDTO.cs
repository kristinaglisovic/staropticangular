using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog.Application.UseCases.DTO.PostTag
{
    public class PostTagDTO
    {
        public int PostId { get; set; }
        public int TagId { get; set; }
        public string PostName { get; set; }
        public string TagName { get; set; }
    }
}
