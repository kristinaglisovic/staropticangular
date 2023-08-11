using Blog.Application.UseCases.DTO.Post;
using Blog.Application.UseCases.DTO.Searches;
using Blog.Application.UseCases.Queries;
using Blog.DataAccess;
using Microsoft.EntityFrameworkCore;
using System.Linq;


namespace Blog.Implementation.UseCases.Queries.Ef
{
    public class EfGetPostsQuery :EfUseCase, IGetPostsQuery
    {
        public EfGetPostsQuery(BlogDbContext context) : base(context)
        {
        }

        public int Id => 1;

        public string Name => "Search Posts";

        public string Description => "Search Posts using Entity Framework";

        public PagedResponse<PostDTO> Execute(PostPagedSearch search)
        {
            var query = Context.Posts.Include(x => x.User).ThenInclude(x => x.Role)
                                     .Include(x => x.Tags)
                                     .ThenInclude(x => x.Tag).ThenInclude(x => x.TagTranslation)
                                     .Include(x => x.Categories)
                                     .ThenInclude(x => x.Category).ThenInclude(x=>x.CategoryTranslation)
                                     .Include(x => x.Comments)
                                     .Include(x => x.Images)
                                     .ThenInclude(x => x.Image)
                                     .Include(x => x.Likes).Include(x=>x.PostTranslation).AsQueryable();
            
                
            if (search.PerPage == -1)
            {
                search.PerPage = query.Count();

            }

           

            if (search.Recent.HasValue)
            {
                if (search.Recent.Value)
                {
                    query = query.Where(p => p.IsActive).OrderByDescending(x=>x.CreatedAt);
                }
                else
                {
                    query = query.Where(p => p.IsActive).OrderBy(x => x.CreatedAt);
                }
            }

            if (search.HasComments.HasValue)
            {
                if (search.HasComments.Value)
                {
                    query = query.Where(p => p.Comments.Any());

                }
                else
                {
                    query = query.Where(p => !p.Comments.Any());
                }
            }


            if (search.IsActive.HasValue) {
                if (search.IsActive.Value)
                {
                    query = query.Where(p => p.IsActive);
                }
                else
                {
                    query = query.Where(p => !p.IsActive);
                }

            }


            if (search.HasLikes.HasValue)
            {
                if (search.HasLikes.Value)
                {
                    query = query.Where(p => p.Likes.Any());

                }
                else
                {
                    query = query.Where(p => !p.Likes.Any());
                }
            }

            var kw = search.Keyword;


            if (!string.IsNullOrEmpty(kw))
            {
                query = query.Where(x => x.User.Username.Contains(kw) ||
                                        x.User.LastName.Contains(kw) ||
                                        x.User.FirstName.Contains(kw) ||
                                        x.Tags.Any(x => x.Tag.Name.Contains(kw)) || x.Tags.Any(x => x.Tag.TagTranslation.NameEng.Contains(kw)) ||
                                        x.Categories.Any(x => x.Category.Name.Contains(kw)) ||
                                        x.Categories.Any(x => x.Category.CategoryTranslation.NameEng.Contains(kw)) || 
                                        x.Images.Any(x => x.Image.Path.Contains(kw)) ||
                                        x.Title.Contains(kw) || x.Description.Contains(kw) || x.PostTranslation.DescriptionEng.Contains(kw) ||x.PostTranslation.TitleEng.Contains(kw));
            }


            if (search.PerPage == null || search.PerPage < 1)
            {
                search.PerPage = 4;
            }

            if(search.Page ==null || search.Page < 1)
            {
                search.Page = 1;
            }


            if (search.CategoryIds != null && search.CategoryIds.Any())
            {
                query = query.Where(x => x.Categories.Any(c => search.CategoryIds.Contains(c.CategoryId)));
            }

            if (search.TagsIds != null && search.TagsIds.Any())
            {
                query = query.Where(x => x.Tags.Any(c => search.TagsIds.Contains(c.TagId)));
            }


            if (search.SortOrder != null)
            {
                switch (search.SortOrder)
                {
                    case PostsOrder.LikesAsc:
                        query = query.OrderBy(x => x.Likes.Where(x=>x.IsActive).Count());
                        break;
                    case PostsOrder.LikesDesc:
                        query = query.OrderByDescending(x => x.Likes.Where(x=>x.IsActive).Count());
                        break;
                    case PostsOrder.CommentsAsc:
                        query = query.OrderBy(x => x.Comments.Where(x => x.IsActive).Count());
                        break;
                    case PostsOrder.CommentsDesc:
                        query = query.OrderByDescending(x => x.Comments.Where(x => x.IsActive).Count());
                        break;
                    default: query = query.OrderByDescending(x => x.CreatedAt);
                        break;
                }
            }


            var toSkip=(search.Page.Value-1)*search.PerPage.Value;

            var response = new PagedResponse<PostDTO>();
            response.TotalCount = query.Count();
            response.Data = query.Skip(toSkip).Take(search.PerPage.Value).Select(x => new PostDTO
            {
                Id = x.Id,
                Title = search.Eng.HasValue && search.Eng.Value ? x.PostTranslation.TitleEng : x.Title,
                TitleSr=x.Title,
                TitleEng = x.PostTranslation.TitleEng,
                Description = search.Eng.HasValue && search.Eng.Value ? x.PostTranslation.DescriptionEng : x.Description,
                DescriptionSr=x.Description,
                DescriptionEng = x.PostTranslation.DescriptionEng,
                Heading1 = search.Eng.HasValue && search.Eng.Value ? x.PostTranslation.Heading1Eng : x.Heading1,
                Heading1Sr=x.Heading1,
                Heading1Eng = x.PostTranslation.Heading1Eng,
                Heading2 = search.Eng.HasValue && search.Eng.Value ? x.PostTranslation.Heading2Eng : x.Heading2,
                Heading2Eng = x.PostTranslation.Heading2Eng,
                Heading2Sr=x.Heading2,
                Text1 = search.Eng.HasValue && search.Eng.Value ? x.PostTranslation.Text1Eng : x.Text1,
                Text1Sr=x.Text1,
                Text1Eng = x.PostTranslation.Text1Eng,
                Text2 = search.Eng.HasValue && search.Eng.Value ? x.PostTranslation.Text2Eng : x.Text2,
                Text2Sr=x.Text2,
                Text2Eng = x.PostTranslation.Text2Eng,
                Images = x.Images.Select(x => x.Image.Path),
                ImagesIds=x.Images.Select(x => x.Image.Id),
                Author = $"{x.User.FirstName} {x.User.LastName} - {x.User.Username}",
                AuthorRole = x.User.Role.Name,
                CommentsCount = x.Comments.Where(y => y.IsActive).Count(),
                Comments = x.Comments.Where(y => y.IsActive).Select(x => new CommentDto
                {
                    Id = x.Id,
                    Comment = x.Text,
                    CommentedAt = x.CreatedAt,
                    User = x.User.Username
                }).ToList(),
                Tags = search.Eng.HasValue && search.Eng.Value ? x.Tags.Select(x => x.Tag.TagTranslation.NameEng) : x.Tags.Select(x => x.Tag.Name),
                TagsIds=x.Tags.Select(x=>x.Tag.Id),
                CategoriesIds=x.Categories.Select(x=>x.Category.Id),
                Categories = search.Eng.HasValue && search.Eng.Value ? x.Categories.Select(x => x.Category.CategoryTranslation.NameEng) : x.Categories.Select(x => x.Category.Name),
                Likes = x.Likes.Where(y => y.IsActive).Count(),
                LikesInfo = x.Likes.Where(x => x.IsActive).Select(x => new LikeDto
                {
                    Id = x.Id,
                    Username = x.User.Username,
                    UserId = x.User.Id
                }).ToList(),
                CreatedAt = x.CreatedAt.ToString("yyyy-MM-dd hh:mm:ss"),
                IsActive = x.IsActive,
                ImagesCount = x.Images.Count(),
             
                UpdatedAt = x.UpdatedAt
            }).ToList();

            response.CurrentPage = search.Page.Value;
            response.ItemsPerPage=search.PerPage.Value;
            

            return response;

            
        }
    }
}
