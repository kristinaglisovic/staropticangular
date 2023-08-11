using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog.Domain.Entities
{
    public class CategoryTranslation:Entity
    {
        public string NameEng { get; set; }
        public string DesciptionEng { get; set; }

        public int CategoryId { get; set; }

        public Category Category { get; set; }
    }
}
