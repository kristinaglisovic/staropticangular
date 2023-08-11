using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog.Domain.Entities
{
    public class Category:Entity
    {
        public string Name { get; set; }
        public string Desciption { get; set; }

        public ICollection<PostCategory> Posts { get; set; } = new List<PostCategory>();

        public CategoryTranslation CategoryTranslation { get; set; }
    }
}
