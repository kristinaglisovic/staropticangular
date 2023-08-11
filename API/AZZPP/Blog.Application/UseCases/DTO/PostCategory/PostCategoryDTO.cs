using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog.Application.UseCases.DTO.PostCategory
{
    public class PostCategoryDTO
    {
        public int PostId { get; set; }
        public string PostName { get; set; }
        public int CategoryId { get; set; }
        public string CategoryName { get; set; }
    }
}
