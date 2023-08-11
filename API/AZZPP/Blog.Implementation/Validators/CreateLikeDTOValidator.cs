using Blog.Application.UseCases.DTO.Like;
using Blog.DataAccess;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog.Implementation.Validators
{
    public class CreateLikeDTOValidator : AbstractValidator<CreateLikeDTO>
    {
        private BlogDbContext _context;

        public CreateLikeDTOValidator(BlogDbContext context)
        {
            _context = context;

            RuleFor(x => x.UserId).Cascade(CascadeMode.Stop).NotEmpty().WithMessage("User id is required.").GreaterThan(0).WithMessage("User id must be greater than 0")
                       .Must(UserNotInUse).WithMessage("User with ID: {PropertyValue} doesn't exist");

            RuleFor(x => x.PostId).Cascade(CascadeMode.Stop).NotEmpty().WithMessage("Post id is required.").GreaterThan(0).WithMessage("Post id must be greater than 0")
                      .Must(PostNotInUse).WithMessage("Post with ID: {PropertyValue} doesn't exist");

        }
        private bool UserNotInUse(int id)
        {
            return _context.Users.Any(x => x.Id == id && x.IsActive);
        }
        private bool PostNotInUse(int id)
        {
            return _context.Posts.Any(x => x.Id == id && x.IsActive);
        }

    }
}
