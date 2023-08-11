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
    public class EfPostActivationCommand : EfUseCase, IPostActivationCommand
    {
        public EfPostActivationCommand(BlogDbContext context) : base(context)
        {
        }

        public int Id => 8;

        public string Name => "Post activation and deactivation";

        public string Description => "Ef Post activation and deactivation";

        public void Execute(int request)
        {
            var post = Context.Posts.Include(x => x.Comments)
                                    .Include(x => x.Likes).Include(x=>x.PostTranslation)
                                    .FirstOrDefault(x => x.Id == request);
            
            
            if (post == null)
            {
                throw new EntityNotFoundException(nameof(Post), request);
            }

            if (post.IsActive)
            {
                var likesToDeactivate = Context.Likes.Where(x => x.PostId == post.Id && x.IsActive).Select(x => x.Id);
                var commentsToDeactivate = Context.Comments.Where(x => x.PostId== post.Id && x.IsActive).Select(x => x.Id);
                if (likesToDeactivate.Any())
                {
                    Context.DeactivateIds<Like>(likesToDeactivate);
                }
                if (commentsToDeactivate.Any())
                {
                    Context.DeactivateIds<Comment>(commentsToDeactivate);
                }

                Context.Deactivate<PostTranslation>(post.PostTranslation.Id);
                Context.Deactivate<Post>(post.Id);
            }
            else
            {
                var likesToActivate = Context.Likes.Where(x => x.PostId == post.Id && !x.IsActive).Select(x => x.Id);
                var commentsToActivate = Context.Comments.Where(x => x.PostId== post.Id && !x.IsActive).Select(x => x.Id);


                if (likesToActivate.Any())
                {
                    Context.ActivateIds<Like>(likesToActivate);
                }
                if (commentsToActivate.Any())
                {
                    Context.ActivateIds<Comment>(commentsToActivate);
                }

                Context.Activate<PostTranslation>(post.PostTranslation.Id);
                Context.Activate<Post>(post.Id);
            }
            Context.SaveChanges();
        }
    }
}
