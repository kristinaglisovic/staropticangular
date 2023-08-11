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
    public class EfDeleteCategoryCommand : EfUseCase, IDeleteCategoryUsingIntCommand
    {
        public EfDeleteCategoryCommand(BlogDbContext context) : base(context)
        {
        }

        public int Id =>26;

        public string Name => "Delete category";

        public string Description => "Delete category by id";

        public void Execute(int id)
        {
            var c = Context.Categories.Include(x => x.Posts).ThenInclude(x => x.Post).FirstOrDefault(x => x.Id == id);


            if (c == null)
            {
                throw new EntityNotFoundException(nameof(Category), id);
            }
            if (c.Posts.Any())
            {
                throw new UseCaseConflictException("Can't delete category because of it's link to post/s: "
                                                  + string.Join(", ", c.Posts.Select(x => x.Post.Title)));
            }

          

            Context.Categories.Remove(c);
            Context.SaveChanges();
        }
    }
}
