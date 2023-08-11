using Blog.Application.UseCases.DTO.PostTag;
using Blog.Application.UseCases.DTO.Searches;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog.Application.UseCases.Queries
{
    public interface IGetPostTagsQuery: IQuery<PostTagsPagedSearch, PagedResponse<PostTagDTO>>
    {
    }
}
