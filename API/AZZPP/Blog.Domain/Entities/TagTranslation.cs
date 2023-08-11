using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog.Domain.Entities
{
    public class TagTranslation:Entity
    {
        public string NameEng { get; set; }

        public int TagId { get; set; }

        public Tag Tag { get; set; }
    }
}
