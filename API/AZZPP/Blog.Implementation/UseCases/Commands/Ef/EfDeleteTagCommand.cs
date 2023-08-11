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
    public class EfDeleteTagCommand : EfUseCase, IDeleteTagUsingIntCommand
    {
        public EfDeleteTagCommand(BlogDbContext context) : base(context)
        {
        }

        public int Id => 23;

        public string Name => "Delete tag";

        public string Description => "EF delete tag by id";

        public void Execute(int id)
        {

            var tag = Context.Tags.Include(x => x.Posts).ThenInclude(x => x.Post).FirstOrDefault(x => x.Id == id);


            if (tag == null)
            {
                throw new EntityNotFoundException(nameof(Tag), id);
            }
            if (tag.Posts.Any())
            {
                throw new UseCaseConflictException("Can't deactivate tag because of it's link to post/s: "
                                                  + string.Join(", ", tag.Posts.Select(x => x.Post.Title)));
            }

            Context.Tags.Remove(tag);
            Context.SaveChanges();
        }
    }
}
