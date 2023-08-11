using Blog.Application.Constants;
using Blog.Application.Emails;
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
    public class EfRegisterUserCommand : EfUseCase, IRegisterUserCommand
    {
        private RegisterValidator _validator;

        private IEmailSender _sender;

        public EfRegisterUserCommand(BlogDbContext context, RegisterValidator validator, IEmailSender sender) : base(context)
        {
            _validator = validator;
            _sender = sender;
        }

        public int Id => 4;

        public string Name => "User Registration";

        public string Description => "User Registration using entity framework";

        public void Execute(RegisterDTO request)
        {
            _validator.ValidateAndThrow(request);

            var hash=BCrypt.Net.BCrypt.HashPassword(request.Password);

            var newUser = new User
            {
                FirstName = request.FirstName,
                LastName = request.LastName,
                Username = request.Username,
                Email = request.Email,
                Password = hash,
                RoleId = request.RoleId,
            };

            if(request.RoleId == 1)
            {
                newUser.UseCases = UseCaseConstants.AdminUserUseCaseIds.Select(x => new UserUseCase
                {
                    UseCaseId = x
                }).ToList();
            }
            else if(request.RoleId == 2)
            {
                newUser.UseCases = UseCaseConstants.RegularUserUseCaseIds.Select(x => new UserUseCase
                {
                    UseCaseId = x
                }).ToList();
            }
        

            if (!string.IsNullOrEmpty(request.ImageFileName))
            {
                var image = new Image
                {
                    Path = request.ImageFileName,
                };
                newUser.Image = image;
            }

            Context.Users.Add(newUser);
            Context.SaveChanges();

            //_sender.SendEmail(new MailDto
            //{
            //    To = request.Email,
            //    Title = "Successfull registration!",
            //    Body = $"Dear {request.Username}{System.Environment.NewLine} Please activate your account..."
            //});
        }
    }
 
}
