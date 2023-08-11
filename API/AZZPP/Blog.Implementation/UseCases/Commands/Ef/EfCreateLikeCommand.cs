using Blog.Application.Exeptions;
using Blog.Application.UseCases.Commands;
using Blog.Application.UseCases.DTO.Like;
using Blog.DataAccess;
using Blog.Domain.Entities;
using Blog.Implementation.Validators;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog.Implementation.UseCases.Commands.Ef
{
    public class EfCreateLikeCommand : EfUseCase, ICreateLikeCommand
    {
        private CreateLikeDTOValidator _validator;
        public EfCreateLikeCommand(BlogDbContext context, CreateLikeDTOValidator validator) : base(context)
        {
            _validator = validator;
        }

        public int Id => 28;

        public string Name => "Like a post";

        public string Description =>"EF like a post";

        public void Execute(CreateLikeDTO request)
        {
            _validator.ValidateAndThrow(request);

            var post=Context.Posts.Find(request.PostId);

            if (Context.Likes.Any(x=>x.UserId==request.UserId && x.PostId==request.PostId))
            {
                throw new UseCaseConflictException($"User {request.UserId} has already liked post with an id {request.PostId}");
            }

            var newLike = new Like
            {
                UserId=request.UserId,
                PostId=request.PostId,
            };

            post.Likes.Add(newLike);
           
            
            Context.SaveChanges();
        }
    }
}
