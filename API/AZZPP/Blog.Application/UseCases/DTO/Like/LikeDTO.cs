using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog.Application.UseCases.DTO.Like
{
    public class LikeDTO:BaseDTO
    {
        public int UserId { get; set; }
        public string Username { get; set; }

        public int TotalLikes { get; set; }
       
        public PostDTO Post { get; set; }

        public bool isActive { get; set; }
    }

    public class PostDTO
    {
        public int Id { get; set; }
        public string Title { get; set; }

        public string CreatedAt { get; set; }
    }
}
