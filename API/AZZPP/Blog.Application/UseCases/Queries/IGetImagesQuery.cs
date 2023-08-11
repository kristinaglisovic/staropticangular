using Blog.Application.UseCases.DTO.Image;
using Blog.Application.UseCases.DTO.Searches;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog.Application.UseCases.Queries
{
    public interface IGetImagesQuery : IQuery<ImagePagedSearch, PagedResponse<ImageDTO>>
    {
    }
}
