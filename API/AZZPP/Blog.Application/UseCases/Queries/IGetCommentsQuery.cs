using Blog.Application.UseCases.DTO.Comment;
using Blog.Application.UseCases.DTO.Searches;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog.Application.UseCases.Queries
{
    public interface IGetCommentsQuery: IQuery<CommentsPagedSearch, PagedResponse<CommentDTO>>
    {
    }
}
