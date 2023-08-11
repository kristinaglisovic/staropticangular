using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog.Domain.Entities
{
    public class Image : Entity
    {
        public string Path { get; set; }

        public User User { get; set; }

        public ICollection<PostImage> Posts { get; set; } = new List<PostImage>();


    }
}
