using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog.Domain.Entities
{
    public class PostImage
    {
        public int PostId { get; set; }
        public int ImageId { get; set; }
        public Image Image { get; set; }
        public Post Post { get; set; }
    }
}
