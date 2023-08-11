using Blog.Application.UseCases.DTO.Roles;
using Blog.DataAccess;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog.Implementation.Validators
{
    public class CreateRoleDTOValidator : AbstractValidator<CreateRoleDTO>
    {
        private BlogDbContext _context;

        public CreateRoleDTOValidator(BlogDbContext context)
        {
            _context = context;
            RuleFor(x => x.Name).NotEmpty().WithMessage("Name is required.").MinimumLength(3).WithMessage("Role name must have at least 3 characters.")
                                 .Must(RoleNotInUse).WithMessage("Role with name '{PropertyValue}' already exists"); ;
        }

        private bool RoleNotInUse(string name)
        {
            return !_context.Roles.Any(x => x.Name == name);
        }
    }
}
