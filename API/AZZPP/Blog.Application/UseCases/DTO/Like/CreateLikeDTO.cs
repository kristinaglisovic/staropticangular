using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog.Application.UseCases.DTO.Like
{
    public class CreateLikeDTO:BaseDTO
    {
        public int UserId { get; set; }
        public int PostId { get; set; }
    }
}
