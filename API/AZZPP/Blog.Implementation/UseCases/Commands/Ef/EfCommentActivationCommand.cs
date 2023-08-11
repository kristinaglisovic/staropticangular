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
    public class EfCommentActivationCommand : EfUseCase, ICommentActivationCommand
    {
        public EfCommentActivationCommand(BlogDbContext context) : base(context)
        {
        }

        public int Id =>36;

        public string Name => "Comment activation and deactivation";

        public string Description => "Ef comment activation and deactivation";

        public void Execute(int id)
        {
            var comment = Context.Comments.FirstOrDefault(x => x.Id == id);

            if (comment == null)
            {
                throw new EntityNotFoundException(nameof(Comment), id);
            }

            if (comment.IsActive) { 
                Context.Deactivate<Comment>(comment.Id);
            }
            else
            {
                Context.Activate<Comment>(comment.Id);

            }
            Context.SaveChanges();


        }
    }
}
