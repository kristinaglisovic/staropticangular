using Blog.Application.Exeptions;
using Blog.Application.UseCases.DTO.Comment;
using Blog.Application.UseCases.DTO.Like;
using Blog.Application.UseCases.Queries;
using Blog.DataAccess;
using Blog.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog.Implementation.UseCases.Queries.Ef
{
    public class EfGetCommentQuery : EfUseCase, IGetCommentQuery
    {
        public EfGetCommentQuery(BlogDbContext context) : base(context)
        {
        }

        public int Id => 35;

        public string Name => "Get comment";

        public string Description => "Get one comment by id";

        public CommentDTO Execute(int id)
        {
            var comment = Context.Comments.Include(x => x.Post).Include(x => x.User).FirstOrDefault(x=>x.Id==id);

            if (comment == null)
            {
                throw new EntityNotFoundException(nameof(Comment), id);
            }


            return new CommentDTO
            {
                Id = comment.Id,
                isActive = comment.IsActive,
                UserId = comment.UserId,
                Username = comment.User.Username,
                CommentedAt = comment.CreatedAt.ToString("yyyy-MM-dd hh:mm:ss"),
                Post =
                         new CommentPostDTO
                         {
                             Title = comment.Post.Title,
                             Id = comment.Post.Id,
                             CreatedAt = comment.Post.CreatedAt.ToString("yyyy-MM-dd hh:mm:ss"),

                         },
                Text = comment.Text
            };
        }
    }
}
