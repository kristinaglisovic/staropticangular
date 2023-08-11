using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog.Domain.Entities
{
    public class Tag:Entity
    {
        public string Name { get; set; }

        public ICollection<PostTag> Posts { get; set; } = new List<PostTag>();

        public TagTranslation TagTranslation { get; set; }

    }
}
