using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog.Application.UseCases.DTO.Category
{
    public class CategoryDTO:BaseDTO
    {
        public string Name { get; set; }
        public string NameSr { get; set; }
        public string NameUs { get; set; }
        public string Description { get; set; }
        public string DescriptionSr { get; set; }
        public string DescriptionUs { get; set; }


        public int PostsCount { get; set; }
        public int ActivePostsCount { get; set; }
        public bool IsActive { get; set; }

        public string CreatedAt { get; set; }
        public string UpdatedAt { get; set; }
    }
}
