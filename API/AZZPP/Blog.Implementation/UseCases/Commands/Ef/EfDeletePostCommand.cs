using Blog.Application.Exeptions;
using Blog.Application.UseCases.Commands;
using Blog.DataAccess;
using Blog.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog.Implementation.UseCases.Commands.Ef
{
    public class EfDeletePostCommand : EfUseCase, IDeletePostUsingIntCommand
    {
        public EfDeletePostCommand(BlogDbContext context) : base(context)
        {
        }

        public int Id => 5;

        public string Name =>"Ef Delete Post";

        public string Description => "Ef delete post by ID";

        public void Execute(int request)
        {
           
                var postToDelete = Context.Posts.Find(request);

                if (postToDelete == null)
                {
                        throw new EntityNotFoundException(nameof(Post), request);
                }


                Context.PostCategories.RemoveRange(postToDelete.Categories);
                Context.PostImages.RemoveRange(postToDelete.Images);
                Context.PostTags.RemoveRange(postToDelete.Tags);

                Context.Posts.Remove(postToDelete);

                Context.SaveChanges();

        }
    }
}
