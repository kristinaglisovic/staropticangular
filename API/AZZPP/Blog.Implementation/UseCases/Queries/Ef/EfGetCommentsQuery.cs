using Blog.Application.UseCases.DTO.Comment;
using Blog.Application.UseCases.DTO.Searches;
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
    public class EfGetCommentsQuery : EfUseCase, IGetCommentsQuery
    {
        public EfGetCommentsQuery(BlogDbContext context) : base(context)
        {
        }

        public int Id => 33;

        public string Name => "Get all comments";

        public string Description => "Ef get all comments";

        public PagedResponse<CommentDTO> Execute(CommentsPagedSearch search)
        {
            var query = Context.Comments.Include(x => x.Post).Include(x => x.User).AsQueryable();


            var kw = search.Keyword;

            if (!string.IsNullOrEmpty(kw))
            {
                query = query.Where(x => x.User.Username.Contains(kw) || x.Post.Title.Contains(kw) || x.User.Likes.Count.ToString().Contains(kw) || x.Text.Contains(kw));

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


            var response = new PagedResponse<CommentDTO>();
            response.TotalCount = query.Count();
            response.Data = query.Skip(toSkip).Take(search.PerPage.Value).Select(x => new CommentDTO
            {
                Id = x.Id,
                isActive = x.IsActive,
                UserId = x.UserId,
                Username = x.User.Username,
                UpdatedAt = x.UpdatedAt,
                CommentedAt = x.CreatedAt.ToString("yyyy-MM-dd hh:mm:ss"),
                Post =
                       new CommentPostDTO
                       {
                           Title = x.Post.Title,
                           Id = x.Post.Id,
                           CreatedAt = x.Post.CreatedAt.ToString("yyyy-MM-dd hh:mm:ss"),
                         
                       },
                Text = x.Text

            }).ToList();

            response.CurrentPage = search.Page.Value;
            response.ItemsPerPage = search.PerPage.Value;


            return response;
        }
    }
}
