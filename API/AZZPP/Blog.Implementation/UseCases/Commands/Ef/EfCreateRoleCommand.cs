using Blog.Application.UseCases.Commands;
using Blog.Application.UseCases.DTO.Roles;
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
    public class EfCreateRoleCommand : EfUseCase, ICreateRoleCommand
    {
        private CreateRoleDTOValidator _validator;
        public EfCreateRoleCommand(BlogDbContext context, CreateRoleDTOValidator validator) : base(context)
        {
            _validator = validator;
        }

        public int Id => 15;

        public string Name => "Create role";

        public string Description => "Ef crate role command";

        public void Execute(CreateRoleDTO request)
        {
            _validator.ValidateAndThrow(request);
            var newRole = new Role
            {
                Name = request.Name
            };
            Context.Roles.Add(newRole);
            Context.SaveChanges();

        }
    }
}
