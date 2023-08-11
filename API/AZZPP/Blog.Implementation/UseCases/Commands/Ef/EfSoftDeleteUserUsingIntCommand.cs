using Blog.Application.Exeptions;
using Blog.Application.UseCases.Commands;
using Blog.DataAccess;
using Blog.DataAccess.Extensions;
using Blog.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog.Implementation.UseCases.Commands.Ef
{
    public class EfSoftDeleteUserUsingIntCommand : EfUseCase, ISoftDeleteUserUsingIntCommand
    {
        public EfSoftDeleteUserUsingIntCommand(BlogDbContext context) : base(context)
        {
        }

        public int Id => 66;

        public string Name => "Soft Delete User";

        public string Description => "Ef Soft Delete User";

        public void Execute(int id)
        {
            var u = Context.Users.Include(x => x.Comments)
                                 .Include(x => x.Likes)
                                 .Include(x => x.Image)
                                 .Include(x => x.Posts)
                                 .FirstOrDefault(x => x.Id == id);




            if (u == null)
            {
                throw new EntityNotFoundException(nameof(User), id);
            }

            var likesToDeactivate = Context.Likes.Where(x => x.UserId == u.Id && x.IsActive).Select(x => x.Id);
            var commentsToDeactivate = Context.Comments.Where(x => x.UserId == u.Id && x.IsActive).Select(x => x.Id);

            var postsToDeactivate = u.Posts.Select(x => x.Id);


            if (likesToDeactivate.Any())
            {
                Context.DeactivateIds<Like>(likesToDeactivate);
            }
            if (commentsToDeactivate.Any())
            {
                Context.DeactivateIds<Comment>(commentsToDeactivate);
            }
            if (postsToDeactivate.Any())
            {
                Context.DeactivateIds<Post>(postsToDeactivate);
            }
            if (u.Image != null)
            {
                Context.Deactivate<Image>(u.Image.Id);
            }

            if (u.IsActive)
            {

                
                Context.Deactivate<User>(u.Id);
            }
           
                Context.SaveChanges();



        }
    }
}
