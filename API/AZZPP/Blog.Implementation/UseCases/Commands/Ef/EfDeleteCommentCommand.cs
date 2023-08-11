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
    public class EfDeleteCommentCommand : EfUseCase, IDeleteCommentUsingIntCommand
    {
        public EfDeleteCommentCommand(BlogDbContext context) : base(context)
        {
        }

        public int Id => 37;

        public string Name => "Delete comment";

        public string Description => "Delete comment EF";

        public void Execute(int id)
        {
            var comment = Context.Comments.Include(x => x.Post).Include(x => x.User).FirstOrDefault(x => x.Id == id);

            if (comment == null)
            {
                throw new EntityNotFoundException(nameof(Comment), id);
            }
            Context.Comments.Remove(comment);
            Context.SaveChanges();

        }
    }
}
