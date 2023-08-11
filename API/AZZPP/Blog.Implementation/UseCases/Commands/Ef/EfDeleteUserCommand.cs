using Blog.Application.Exeptions;
using Blog.Application.UseCases.Commands;
using Blog.DataAccess;
using Blog.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog.Implementation.UseCases.Commands.Ef
{
    public class EfDeleteUserCommand : EfUseCase, IDeleteUserUsingIntCommand
    {
        public EfDeleteUserCommand(BlogDbContext context) : base(context)
        {
        }

        public int Id => 41;
        public string Name => "Delete user";

        public string Description => "Delete user by id EF";

        public void Execute(int id)
        {
            var u = Context.Users
                                  .Include(x => x.Comments)
                                  .Include(x => x.Likes)
                                  .Include(x => x.Image).FirstOrDefault(x => x.Id == id);




            if (u == null)
            {
                throw new EntityNotFoundException(nameof(User), id);
            }




            var likesToDelete = Context.Likes.Where(x => x.UserId == u.Id && x.IsActive);
            var commentsToDelete = Context.Comments.Where(x => x.UserId == u.Id && x.IsActive);
            //   var postsToDelete = Context.Posts.Where(x => x.UserId == u.Id);
            var userUseCases = Context.UserUseCases.Where(x => x.UserId == u.Id);

            if (likesToDelete.Any())
            {
                Context.Likes.RemoveRange(likesToDelete);
            }
            if (commentsToDelete.Any())
            {
                Context.Comments.RemoveRange(commentsToDelete) ;
            } 
            //if (postsToDelete.Any())
            //{
            //    Context.Posts.RemoveRange(postsToDelete) ;
            //}
            if (u.Image != null)
            {
                Context.Images.Remove(u.Image);
            }

            Context.UserUseCases.RemoveRange(userUseCases);


            Context.Users.Remove(u);
            Context.SaveChanges();
        }
    }
}
