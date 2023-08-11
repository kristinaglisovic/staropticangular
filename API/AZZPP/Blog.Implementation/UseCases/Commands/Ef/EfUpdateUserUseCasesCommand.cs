using Blog.Application.UseCases.Commands;
using Blog.Application.UseCases.DTO.User;
using Blog.DataAccess;
using Blog.Domain;
using Blog.Implementation.Validators;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog.Implementation.UseCases.Commands.Ef
{
    public class EfUpdateUserUseCasesCommand : EfUseCase, IUpdateUserUseCasesCommand
    {
        private UpdateUserUseCaseValidator _validator;

        public EfUpdateUserUseCasesCommand(BlogDbContext context, UpdateUserUseCaseValidator validator) : base(context)
        {
            _validator = validator;
        }

        public int Id => 6;

        public string Name => "User UseCase Update";

        public string Description => "Update user use case command using EF";

        public void Execute(UpdateUserUseCasesDTO request)
        {
            _validator.ValidateAndThrow(request);

            var useCases = Context.UserUseCases.Where(x => x.UserId == request.UserId);

            Context.RemoveRange(useCases);

            var newUseCases = request.UseCaseIds.Select(x => new UserUseCase
            {
                UseCaseId=x,
                UserId=request.UserId,
                UpdatedAt=DateTime.Now,
            });

            Context.UserUseCases.AddRange(newUseCases);

            Context.SaveChanges();

        }
    }
}
