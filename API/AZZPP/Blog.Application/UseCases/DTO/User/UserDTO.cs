using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog.Application.UseCases.DTO.User
{
    public class UserDTO : BaseDTO
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public string Role { get; set; }
        public int RoleId { get; set; }

        public int PostsCount { get; set; }
        public int ActivePostsCount { get; set; }
        public int LikesCount { get; set; }
        public int CommentsCount { get; set; }
        public bool IsActive { get; set; }

        public string Image { get; set; }
        public int? ImageId { get; set; }

        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
    }

       
}
