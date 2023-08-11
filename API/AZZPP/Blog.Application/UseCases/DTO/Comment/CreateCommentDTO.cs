using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog.Application.UseCases.DTO.Comment
{
    public class CreateCommentDTO:BaseDTO
    {
        public int UserId { get; set; }
        public int PostId { get; set; }
        public string Text { get; set; }
    }
}
