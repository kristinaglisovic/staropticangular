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
    public class EfUserActivationCommand : EfUseCase, IUserActivationCommand
    {
        public EfUserActivationCommand(BlogDbContext context) : base(context)
        {
        }

        public int Id => 72;

        public string Name => "User activation and deactivation";

        public string Description => "User activation and deactivation EF";

        public void Execute(int id)
        {

            var u = Context.Users.Include(x => x.Comments)
                                 .Include(x => x.Likes)
                                 .Include(x => x.Image).FirstOrDefault(x => x.Id == id);


           

            if (u == null)
            {
                throw new EntityNotFoundException(nameof(User), id);
            }

            if (u.IsActive)
            {

                var likesToDeactivate=Context.Likes.Where(x=>x.UserId==u.Id && x.IsActive).Select(x=>x.Id);
                var commentsToDeactivate=Context.Comments.Where(x=>x.UserId==u.Id && x.IsActive).Select(x => x.Id);


                if (likesToDeactivate.Any())
                {
                    Context.DeactivateIds<Like>(likesToDeactivate);
                }
                if (commentsToDeactivate.Any())
                { 
                  Context.DeactivateIds<Comment>(commentsToDeactivate);
                }
                if (u.Image != null)
                {
                  Context.Deactivate<Image>(u.Image.Id);
                }
                Context.Deactivate<User>(u.Id);
            }
            else
            {

                var likesToActivate = Context.Likes.Where(x => x.UserId == u.Id && !x.IsActive).Select(x => x.Id);
                var commentsToActivate = Context.Comments.Where(x => x.UserId == u.Id && !x.IsActive).Select(x => x.Id);


                if (likesToActivate.Any())
                {
                    Context.ActivateIds<Like>(likesToActivate);
                }
                if (commentsToActivate.Any())
                {
                    Context.ActivateIds<Comment>(commentsToActivate);
                }
                if (u.Image != null)
                {
                    Context.Activate<Image>(u.Image.Id);
                }
                Context.Activate<User>(u.Id);

            }




            Context.SaveChanges();
           
        }
    }
}
