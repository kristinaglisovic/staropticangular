using Blog.Application.UseCases.DTO.Post;
using Blog.Application.UseCases.DTO.Searches;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog.Application.UseCases.Queries
{
    public interface IGetPostsQuery : IQuery<PostPagedSearch,PagedResponse<PostDTO>>
    {
    }

    //

  
}
