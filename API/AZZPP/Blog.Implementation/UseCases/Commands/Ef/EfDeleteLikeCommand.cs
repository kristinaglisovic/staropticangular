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
    public class EfDeleteLikeCommand : EfUseCase, IDeleteLikeUsingIntCommand
    {
        public EfDeleteLikeCommand(BlogDbContext context) : base(context)
        {
        }

        public int Id => 45;

        public string Name => "Delete like";

        public string Description => "Delete like EF";

        public void Execute(int id)
        {
            var like = Context.Likes.Include(x => x.Post).Include(x => x.User).FirstOrDefault(x => x.Id == id);


            if (like == null)
            {
                throw new EntityNotFoundException(nameof(Like), id);
            }


            Context.Likes.Remove(like);
            Context.SaveChanges();
        }
    }
}
