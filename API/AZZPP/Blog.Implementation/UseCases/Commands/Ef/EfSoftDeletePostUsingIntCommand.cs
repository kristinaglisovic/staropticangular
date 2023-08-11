using Blog.Application.UseCases.Commands;
using Blog.DataAccess;
using Blog.Domain.Entities;
using Blog.Application.Exeptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Blog.DataAccess.Extensions;
using Microsoft.EntityFrameworkCore;

namespace Blog.Implementation.UseCases.Commands.Ef
{
    public class EfSoftDeletePostUsingIntCommand : EfUseCase, ISoftDeletePostUsingIntCommand
    {
        public EfSoftDeletePostUsingIntCommand(BlogDbContext context) : base(context)
        {
        }

        public int Id => 64;

        public string Name => "Post deactivation";

        public string Description => "Ef post deactivation";

        public void Execute(int request)
        {
            var post = Context.Posts.Include(x => x.Comments)
                                    .Include(x => x.Likes)
                                    .FirstOrDefault(x => x.Id == request);


            if (post == null)
            {
                throw new EntityNotFoundException(nameof(Post), request);
            }

            if (post.IsActive)
            {
                var likesToDeactivate = Context.Likes.Where(x => x.PostId == post.Id && x.IsActive).Select(x => x.Id);
                var commentsToDeactivate = Context.Comments.Where(x => x.PostId == post.Id && x.IsActive).Select(x => x.Id);
                if (likesToDeactivate.Any())
                {
                    Context.DeactivateIds<Like>(likesToDeactivate);
                }
                if (commentsToDeactivate.Any())
                {
                    Context.DeactivateIds<Comment>(commentsToDeactivate);
                }
                Context.Deactivate<Post>(post.Id);
                Context.SaveChanges();
            }
            
        }
    }
}
