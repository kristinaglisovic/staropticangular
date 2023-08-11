using Blog.Application.UseCases.DTO.Category;
using Blog.Application.UseCases.DTO.Searches;

namespace Blog.Application.UseCases.Queries
{
    public interface IGetCategoriesQuery: IQuery<CategoriesPagedSearch, PagedResponse<CategoryDTO>>
    {
    }
}
