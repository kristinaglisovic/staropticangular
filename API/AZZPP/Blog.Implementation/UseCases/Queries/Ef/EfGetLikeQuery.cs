using Blog.Application.Exeptions;
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
    public class EfGetLikeQuery : EfUseCase, IGetLikeQuery
    {
        public EfGetLikeQuery(BlogDbContext context) : base(context)
        {
        }

        public int Id => 31;

        public string Name => "Get like";

        public string Description => "Ef get like";

        public LikeDTO Execute(int id)
        {
            var l= Context.Likes.Include(x => x.Post).Include(x => x.User).FirstOrDefault(x=>x.Id==id);
            if (l == null)
            {
                throw new EntityNotFoundException(nameof(Like), id);
            }

            return new LikeDTO
            {
                Id = l.Id,
                isActive = l.IsActive,
                UserId = l.UserId,
                Username = l.User.Username,
                TotalLikes = l.User.Likes.Count,
                Post =
                       new PostDTO
                       {
                           Title = l.Post.Title,
                           Id = l.Post.Id,
                           CreatedAt = l.Post.CreatedAt.ToString("yyyy-MM-dd hh:mm:ss"),
                       }
            };

        }
    }
}
