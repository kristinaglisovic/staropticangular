using Blog.Application.UseCases.DTO.Searches;
using Blog.Application.UseCases.DTO.User;
using Blog.Application.UseCases.Queries;
using Blog.DataAccess;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog.Implementation.UseCases.Queries.Ef
{
    public class EfGetUsersQuery : EfUseCase, IGetUsersQuery
    {
        public EfGetUsersQuery(BlogDbContext context) : base(context)
        {
        }

        public int Id => 38;

        public string Name => "Get all users";

        public string Description => "Ef get all users";

        public PagedResponse<UserDTO> Execute(UsersPagedSearch search)
        { 

            var query=Context.Users.Include(x=>x.Posts).Include(x=>x.Comments).Include(x=>x.Likes).Include(x=>x.Image).AsQueryable();


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

            if (search.HasPosts.HasValue)
            {
                if (search.HasPosts.Value)
                {
                    query = query.Where(p => p.Posts.Any());

                }
                else
                {
                    query = query.Where(p => !p.Posts.Any());
                }
            }
            if (search.HasImage.HasValue)
            {
                if (search.HasImage.Value)
                {
                    query = query.Where(p => p.Image !=null);

                }
                else
                {
                    query = query.Where(p => p.Image == null);
                }
            }

            var kw = search.Keyword;


            if (!string.IsNullOrEmpty(kw))
            {
                query = query.Where(x => x.Username.Contains(kw) ||
                                     x.LastName.Contains(kw) ||
                                     x.FirstName.Contains(kw) ||
                                     x.Email.Contains(kw) ||
                                     x.Role.Name.Contains(kw) || x.IsActive.ToString().Contains(kw));
            }

            if (search.PerPage == null || search.PerPage < 1)
            {
                search.PerPage = 5;
            }

            if (search.Page == null || search.Page < 1)
            {
                search.Page = 1;
            }


            var toSkip = (search.Page.Value - 1) * search.PerPage.Value;

            var response = new PagedResponse<UserDTO>();
            response.TotalCount = query.Count();
            response.Data = query.Skip(toSkip).Take(search.PerPage.Value).Select(x => new UserDTO
            {
                Id = x.Id,
                FirstName= x.FirstName,
                LastName= x.LastName,
                Username= x.Username,
                Email= x.Email,
                Role= x.Role.Name,
                IsActive= x.IsActive,
                ActivePostsCount=x.Posts.Where(x=>x.IsActive).Count(),
                PostsCount = x.Posts.Count(),
                LikesCount=x.Likes.Where(x=>x.IsActive).Count(),
                CommentsCount=x.Comments.Where(x=>x.IsActive).Count(),
                Image= x.Image != null ? $"{x.Image.Path}" : "No image" ,
                CreatedAt=x.CreatedAt,
                UpdatedAt=x.UpdatedAt,
                RoleId=x.Role.Id,
                ImageId=x.Image.Id,
            }).ToList();

            response.CurrentPage = search.Page.Value;
            response.ItemsPerPage = search.PerPage.Value;


            return response;

        }
    }
}
