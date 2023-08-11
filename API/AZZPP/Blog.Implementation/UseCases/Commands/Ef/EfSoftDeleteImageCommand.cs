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
    public class EfSoftDeleteImageCommand : EfUseCase, ISoftDeleteImageUsingIntCommand
    {
        public EfSoftDeleteImageCommand(BlogDbContext context) : base(context)
        {
        }

        public int Id => 71;

        public string Name => "Soft Delete Image";

        public string Description => "Ef Soft Delete Image using deactivation";

        public void Execute(int request)
        {
            var image = Context.Images.Where(x => x.IsActive)
                                      .Include(x => x.User).Include(x => x.Posts)
                                      .ThenInclude(x => x.Post).FirstOrDefault(x=>x.Id== request);
            if (image == null)
            {
                throw new EntityNotFoundException(nameof(Image), request);
            }


            if (image.User != null)
            {
                throw new UseCaseConflictException($"Can't deactivate image because of it's link to user: {image.User.Username}");
            }

            if (image.Posts.Any())
            {
                throw new UseCaseConflictException("Can't deactivate image because of it's link to posts: "
                                                  + string.Join(", ", image.Posts.Select(x => x.Post.Title)));
            }

            Context.Deactivate<Image>(image.Id);
            Context.SaveChanges();
        }
    }
}
