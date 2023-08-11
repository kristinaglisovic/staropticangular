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
    public class EfTagActivationCommand : EfUseCase, ITagActivationCommand
    {
        public EfTagActivationCommand(BlogDbContext context) : base(context)
        {
        }

        public int Id => 21;

        public string Name => "Tag activation and deactivation";

        public string Description => "Ef Tag activation and deactivation";

        public void Execute(int id)
        {
            var tag = Context.Tags.Include(x => x.Posts).ThenInclude(x=>x.Post).Include(x=>x.TagTranslation).FirstOrDefault(x => x.Id == id);


            if (tag == null)
            {
                throw new EntityNotFoundException(nameof(Tag), id);
            }
            if (tag.IsActive)
            {
                    if (tag.Posts.Any())
                    {
                        throw new UseCaseConflictException("Can't deactivate tag because of it's link to post/s: "
                                                          + string.Join(", ", tag.Posts.Select(x => x.Post.Title)));
                    }


                 
                
                
                
                Context.Deactivate<TagTranslation>(tag.TagTranslation.Id);


                Context.Deactivate<Tag>(tag.Id);
            }
            else
            {
                Context.Activate<TagTranslation>(tag.TagTranslation.Id);
                Context.Activate<Tag>(tag.Id);
            }

            Context.SaveChanges();

        }
    }
}
