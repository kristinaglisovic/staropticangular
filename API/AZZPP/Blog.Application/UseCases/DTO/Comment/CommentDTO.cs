using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog.Application.UseCases.DTO.Comment
{
    public class CommentDTO:BaseDTO
    {
        public int UserId { get; set; }
        public string Username { get; set; }
        public string Text { get; set; }
        public bool isActive{ get; set; }
        public CommentPostDTO Post { get; set; }
        public string CommentedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
    }
    public class CommentPostDTO
    {
        public int Id { get; set; }
        public string Title { get; set; }

        public string CreatedAt { get; set; }
    }

}
