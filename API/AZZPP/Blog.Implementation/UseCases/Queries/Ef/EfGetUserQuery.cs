using Blog.Application.Exeptions;
using Blog.Application.UseCases.DTO.User;
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
    public class EfGetUserQuery : EfUseCase, IGetUserQuery
    {
        public EfGetUserQuery(BlogDbContext context) : base(context)
        {
        }

        public int Id => 39;

        public string Name => "Get User";

        public string Description => "Get user by ID EF";

        public UserDTO Execute(int id)
        {
            var u = Context.Users.Include(x => x.Posts).Include(x => x.Comments).Include(x=>x.Role).Include(x => x.Likes).Include(x => x.Image).FirstOrDefault(x => x.Id == id);

            if (u == null || !u.IsActive)
            {
                throw new EntityNotFoundException(nameof(User), id);
            }

            return new UserDTO
            {
                Id = u.Id,
                FirstName = u.FirstName,
                LastName = u.LastName,
                Username = u.Username,
                Email = u.Email,
                Role = u.Role.Name,
                IsActive = u.IsActive,
                PostsCount = u.Posts.Where(x => x.IsActive).Count(),
                LikesCount = u.Likes.Where(x => x.IsActive).Count(),
                CommentsCount = u.Likes.Where(x => x.IsActive).Count(),
                Image = u.Image != null ? $"{u.Image.Path}" : "No image",
                ImageId=u.Image !=null ? u.ImageId : null,
                RoleId=u.Role.Id
            };
        }
    }
}
