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
    public class EfSoftDeleteLikeCommand : EfUseCase, ISoftDeleteLikeUsingIntCommand
    {
        public EfSoftDeleteLikeCommand(BlogDbContext context) : base(context)
        {
        }

        public int Id => 32;

        public string Name => "Soft delete like";

        public string Description => "Like deactivation using EF";

        public void Execute(int id)
        {
            var like = Context.Likes.Include(x => x.Post).Include(x => x.User).FirstOrDefault(x=>x.Id==id);


            if (like == null || !like.IsActive)
            {
                throw new EntityNotFoundException(nameof(Like), id);
            }


            Context.Deactivate<Like>(like.Id);
            Context.SaveChanges();
        }
    }
}
