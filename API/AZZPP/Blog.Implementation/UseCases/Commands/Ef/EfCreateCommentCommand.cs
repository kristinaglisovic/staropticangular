using Blog.Application.Exeptions;
using Blog.Application.UseCases.Commands;
using Blog.Application.UseCases.DTO.Comment;
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
    public class EfCreateCommentCommand : EfUseCase, ICreateCommentCommand
    {
        private CreateCommentDTOValidator _validator;

        public EfCreateCommentCommand(BlogDbContext context, CreateCommentDTOValidator validator) : base(context)
        {
            _validator = validator;
        }

        public int Id => 29;

        public string Name => "Create a comment";

        public string Description => "Create a comment EF";

        public void Execute(CreateCommentDTO request)
        {
            _validator.ValidateAndThrow(request);

            var post = Context.Posts.Find(request.PostId);

            if (Context.Comments.Any(x => x.UserId == request.UserId && x.PostId == request.PostId && x.Text.Equals(request.Text)))
            {
                //throw new UseCaseConflictException($"User {request.UserId} has already commented post {request.PostId} with an text '{request.Text}'");
                throw new UseCaseConflictException($"Već ste ostavili komentar sa tekstom '{request.Text}'");
            }

            var newComm = new Comment
            {
                UserId = request.UserId,
                PostId = request.PostId,
                Text=request.Text
            };

            post.Comments.Add(newComm);


            Context.SaveChanges();
        }
    }
}
