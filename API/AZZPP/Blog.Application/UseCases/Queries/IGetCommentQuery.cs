using Blog.Application.UseCases.DTO.Comment;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog.Application.UseCases.Queries
{
    public interface IGetCommentQuery : IQuery<int, CommentDTO>
    {
    }
}
