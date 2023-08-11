using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog.Domain.Entities
{
    public class User:Entity
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }

        public int RoleId { get; set; }

        public int? ImageId { get; set; }

        public Role Role { get; set; }
        public Image Image { get; set; }


        public ICollection<Post> Posts { get; set; } = new List<Post>();
        public ICollection<Comment> Comments { get; set; } = new List<Comment>();

        public ICollection<Like> Likes { get; set; } = new List<Like>();

        public ICollection<UserUseCase> UseCases { get; set; } = new List<UserUseCase>();



    }
}
