using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog.Domain.Entities
{
    public class PostTranslation:Entity
    {
        public string TitleEng { get; set; }
        public string DescriptionEng { get; set; }
        public string Heading1Eng { get; set; }
        public string Text1Eng { get; set; }

        public string Heading2Eng { get; set; }
        public string Text2Eng { get; set; }
        public int PostId { get; set; }
        public Post Post { get; set; }

    }
}
