using Blog.Application.UseCases.DTO.Post;
using Blog.Application.UseCases.DTO.Searches;
using Blog.Application.UseCases.Queries;
using Blog.DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Blog.Application.Exeptions;
using Microsoft.EntityFrameworkCore;
using Blog.Domain.Entities;

namespace Blog.Implementation.UseCases.Queries.Ef
{
    public class EfGetPostQuery : EfUseCase, IGetPostQuery
    {
        public EfGetPostQuery(BlogDbContext context) : base(context)
        {
        }

        public int Id => 2;

        public string Name => "EF Find post";

        public string Description => "Get post by using an ID";


        public PostDTO Execute(SingleTranslatedSearch search)
        {

            var x = Context.Posts.Where(x => x.IsActive)
                                   .Include(x => x.User).ThenInclude(x => x.Role)
                                   .Include(x => x.Tags)
                                   .ThenInclude(x => x.Tag).ThenInclude(x => x.TagTranslation)
                                   .Include(x => x.Categories)
                                   .ThenInclude(x => x.Category).ThenInclude(x=>x.CategoryTranslation)
                                   .Include(x => x.Comments).ThenInclude(x => x.User).ThenInclude(x => x.Image)
                                   .Include(x => x.Images)
                                   .ThenInclude(x => x.Image)
                                   .Include(x => x.Likes).ThenInclude(x => x.User).Include(x=>x.PostTranslation).FirstOrDefault(x => x.Id == search.id);


            if (x == null)
            {
                throw new EntityNotFoundException(nameof(Post), search.id);
            }

            return new PostDTO
            {
                Text2 = search.eng.HasValue && search.eng.Value ? x.PostTranslation.Text2Eng : x.Text2,
                Text1 = search.eng.HasValue && search.eng.Value ? x.PostTranslation.Text1Eng : x.Text1,
                Heading1 = search.eng.HasValue && search.eng.Value ? x.PostTranslation.Heading1Eng : x.Heading1,
                Heading2 = search.eng.HasValue && search.eng.Value ? x.PostTranslation.Heading2Eng : x.Heading2,
                Id = x.Id,
                Title = search.eng.HasValue && search.eng.Value ? x.PostTranslation.TitleEng  : x.Title,
                Description = search.eng.HasValue && search.eng.Value ? x.PostTranslation.DescriptionEng : x.Description,
                Images = x.Images.Select(x => x.Image.Path),
                Author = $"{x.User.FirstName} {x.User.LastName} - {x.User.Username}",
                AuthorRole = x.User.Role.Name,
                CommentsCount = x.Comments.Where(y => y.IsActive).Count(),
                Comments = x.Comments.Where(y => y.IsActive).OrderByDescending(x => x.CreatedAt).Select(x => new CommentDto
                {
                    Id = x.Id,
                    Comment = x.Text,
                    CommentedAt = x.CreatedAt,
                    User = x.User.Username,
                    Image = x.User.Image != null ? x.User.Image.Path : "",
                }).ToList(),
                LikesInfo = x.Likes.Where(x => x.IsActive).Select(x => new LikeDto
                {
                    Id = x.Id,
                    Username = x.User.Username,
                    UserId = x.User.Id
                }).ToList(),
                Tags = search.eng.HasValue && search.eng.Value ? x.Tags.Select(x=>x.Tag.TagTranslation.NameEng) : x.Tags.Select(x => x.Tag.Name),
                TagsIds = x.Tags.Select(x => x.Tag.Id).ToList(),
                CategoriesIds = x.Categories.Select(x => x.Category.Id).ToList(),
                ImagesIds = x.Images.Select(x => x.ImageId).ToList(),
                Categories = search.eng.HasValue && search.eng.Value ? x.Categories.Select(x => x.Category.CategoryTranslation.NameEng) :  x.Categories.Select(x => x.Category.Name),
                Likes = x.Likes.Where(y => y.IsActive).Count(),
                CreatedAt = x.CreatedAt.ToString("yyyy-MM-dd hh:mm:ss"),


            };
        }
    }
}