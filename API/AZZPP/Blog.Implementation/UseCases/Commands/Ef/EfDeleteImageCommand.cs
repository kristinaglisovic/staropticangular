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
    public class EfDeleteImageCommand : EfUseCase, IDeleteImageUsingIntCommand
    {
        public EfDeleteImageCommand(BlogDbContext context) : base(context)
        {
        }

        public int Id => 12;

        public string Name => "Delete image";

        public string Description => "Ef delete image by ID";

        public void Execute(int request)
        {
            var imageToDelete = Context.Images
                                     .Include(x => x.User).Include(x => x.Posts)
                                     .ThenInclude(x => x.Post).FirstOrDefault(x => x.Id == request);

            if (imageToDelete == null)
            {
                throw new EntityNotFoundException(nameof(Image), request);
            }


            //if (imageToDelete.User != null)
            //{
            //    throw new UseCaseConflictException($"Can't delete image because of it's link to user: {imageToDelete.User.Username}");
            //}

            if (imageToDelete.Posts.Any())
            {
                throw new UseCaseConflictException("Can't delete image because of it's link to posts: "
                                                  + string.Join(", ", imageToDelete.Posts.Select(x => x.Post.Title)));
            }

            Context.Images.Remove(imageToDelete);

            Context.SaveChanges();
        }
    }
}
