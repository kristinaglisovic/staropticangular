using Blog.Application.UseCases.Commands;
using Blog.Application.UseCases.DTO.Image;
using Blog.DataAccess;
using Blog.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog.Implementation.UseCases.Commands.Ef
{
    public class EfCreateImageCommand : EfUseCase, ICreateImageCommand
    {
        public EfCreateImageCommand(BlogDbContext context) : base(context)
        {
        }

        public int Id => 11;

        public string Name => "Image upload in database";

        public string Description => "Ef create image by uploading photo";

        public void Execute(ImageDTO request)
        {
            Context.Images.Add(new Image
            {
                Path = request.Path
            });

            Context.SaveChanges();
               
        }
    }
}
