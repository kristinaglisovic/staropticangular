using Blog.Application.Constants;
using Blog.Application.Exeptions;
using Blog.Application.UseCases.Commands;
using Blog.Application.UseCases.DTO.User;
using Blog.DataAccess;
using Blog.Domain;
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
    public class EfEditUserCommand : EfUseCase, IEditUserCommand
    {
        private EditUserValidator _validator;
        public EfEditUserCommand(BlogDbContext context, EditUserValidator validator) : base(context)
        {
            _validator = validator;
        }

        public int Id => 69;

        public string Name => "Edit a user";

        public string Description => "Ef edit user";

        public void Execute(UserEditDTO request)
        {
            var user = Context.Users.Find(request.UserId);
            var updated = false;


            _validator.ValidateAndThrow(request);

            if (user == null)
            {
                throw new EntityNotFoundException(nameof(User), user.Id);
            }

            if (user.FirstName != request.FirstName)
            {
                user.FirstName = request.FirstName;
                updated = true;
            }

            if (user.LastName != request.LastName)
            {
                user.LastName = request.LastName;
                updated = true;
            }



            if (user.Username != request.Username && !Context.Users.Any(x => x.Username.Equals(request.Username)))
            {
                user.Username = request.Username;
                updated = true;
            }
            else if (user.Username != request.Username && Context.Users.Any(x => x.Username.Equals(request.Username)))
            {
                throw new EditValidationException($"Već postoji korisnik sa kor.imenom '{request.Username}'","username");

            }

            if (user.Email != request.Email && !Context.Users.Any(x => x.Email.Equals(request.Email)))
            {
                user.Email = request.Email;
                updated = true;
            }
            else if (user.Email != request.Email && Context.Users.Any(x => x.Email.Equals(request.Email)))
            {
                throw new EditValidationException($"Već postoji korisnik sa e-mailom '{request.Email}'", "email");

            }


            if (request.RoleId!=0) { 
            if (user.RoleId != request.RoleId)
            {
                var userUseCases = Context.UserUseCases.Where(x => x.UserId == user.Id).ToList();
                Context.UserUseCases.RemoveRange(userUseCases);
                Context.SaveChanges();

                if (request.RoleId == 1)
                {
                    user.UseCases = UseCaseConstants.AdminUserUseCaseIds.Select(x => new UserUseCase
                    {
                        UseCaseId = x
                    }).ToList();
                }
                else if(request.RoleId == 2)
                {
                    user.UseCases = UseCaseConstants.RegularUserUseCaseIds.Select(x => new UserUseCase
                    {
                        UseCaseId = x
                    }).ToList();
                }
                user.RoleId = request.RoleId;
                updated = true;
            }
            }


            if (!string.IsNullOrEmpty(request.ImageFileName))
            {
                var image = new Image
                {
                    Path = request.ImageFileName,
                };
                user.Image = image;
                updated = true;
            }



            if (updated)
            {
                user.UpdatedAt = DateTime.Now;
                Context.SaveChanges();
            }



        }
    }
}
