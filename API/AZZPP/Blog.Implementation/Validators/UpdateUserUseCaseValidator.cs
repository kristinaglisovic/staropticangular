using Blog.Application.UseCases.DTO.User;
using Blog.DataAccess;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog.Implementation.Validators
{
    public class UpdateUserUseCaseValidator:AbstractValidator<UpdateUserUseCasesDTO>
    { 
        private BlogDbContext _context;

        public UpdateUserUseCaseValidator(BlogDbContext context)
        {
            _context = context;

            RuleFor(x => x.UserId).Cascade(CascadeMode.Stop).NotEmpty().WithMessage("User id is required")
                        .Must(x => context.Users.Any(y => y.Id == x && y.IsActive))
                        .WithMessage("User with provided ID doesn't exist.");

            RuleFor(x => x.UseCaseIds).Cascade(CascadeMode.Stop).NotEmpty().WithMessage("Use cases are required.")
                                      .Must(x=> x.Distinct().Count()==x.Count()).WithMessage("Duplicates are not allowed"); 

        }

        private bool UserNotInUse(int id)
        {
            return _context.Users.Any(x => x.Id == id && x.IsActive);
        }

    }
}
