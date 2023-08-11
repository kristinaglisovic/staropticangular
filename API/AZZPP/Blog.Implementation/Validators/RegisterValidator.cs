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
    public class RegisterValidator:AbstractValidator<RegisterDTO>
    {
        private BlogDbContext _context;
        public RegisterValidator(BlogDbContext context)
        {
            var firstLastNameReg = @"^[A-ZŠĐČĆŽa-zšđčćž]+((\s)?((\'|\-|\.)?([A-ZŠĐČĆŽa-zšđčćž])+))*$";
            //Jack Alexander is valid, Sierra O'Neil is valid
            _context = context;
            RuleFor(x => x.FirstName).Cascade(CascadeMode.Stop).NotEmpty().WithMessage("First name is required.")
                                  .MinimumLength(2).WithMessage("First name must have at least 2 characters.")
                                  .MaximumLength(40).WithMessage("First name cannot be longer than 40 characters.")
                                  .Matches(firstLastNameReg)
                                  .WithMessage("You can use letters, special characters and spaces (ex. Sierra O'Neil)");              
            
            RuleFor(x=>x.LastName).Cascade(CascadeMode.Stop).NotEmpty().WithMessage("Last name is required.")
                                      .MinimumLength(3).WithMessage("Last name must have at least 3 characters.")
                                      .MaximumLength(40).WithMessage("Last name cannot be longer than 40 characters.")
                                      .Matches(firstLastNameReg)
                                      .WithMessage("You can use letters, special characters and spaces (ex. Sierra O'Neil)"); ;
            

            RuleFor(x=>x.Username).Cascade(CascadeMode.Stop).NotEmpty().WithMessage("Username is required.")
                                     .MinimumLength(4).WithMessage("Username must have at least 4 characters.")
                                     .MaximumLength(25).WithMessage("Username cannot be longer than 25 characters.")
                                     .Must(x => !context.Users.Any(u => u.Username == x)).WithMessage("Korisničko ime '{PropertyValue}' je zauzeto.")
                                     .Matches(@"^(?=.{4,30}$)(?![_.])(?!.*[_.]{2})[a-zšđčćžA-ZŠĐČĆŽ0-9._]+(?<![_.])$")
                                     .WithMessage("Invalid username format. Username must be 4-30 characters long." +
                                     "You can use letters, numbers, . and _");
            


           RuleFor(x => x.Email).Cascade(CascadeMode.Stop).NotEmpty().WithMessage("Email is required.")
                                     .EmailAddress().WithMessage("Value '{PropertyValue}' is not a valid format of email")
                                     .Must(x => !context.Users.Any(u => u.Email == x))
                                     .WithMessage("E-mail '{PropertyValue}' je već registrovan.");
                
            
            RuleFor(x=>x.Password).Cascade(CascadeMode.Stop).NotEmpty().WithMessage("Password is required.")
                                      .Matches(@"^(?=.*[a-zšđčćž])(?=.*[A-ZŠĐČĆŽ])(?=.*\d)(?=.*[@$!%*?&])[A-ZŠĐČĆŽa-zšđčćž\d@$!%*?&]{8,15}$")
                                      .WithMessage("Invalid password format: 8-15 characters with at least one uppercase letter, " +
                                      "one lowercase letter, one number and one special character:");
           
            
            
            RuleFor(x => x.RoleId).Cascade(CascadeMode.Stop).GreaterThan(0).WithMessage("Role id must be greater than 0")
                                  .Must(x => context.Roles.Any(c => c.Id == x)).WithMessage("Role with ID: {PropertyValue} doesn't exist");

            /*RuleFor(x => x.ImageId).Cascade(CascadeMode.Stop).GreaterThan(0).WithMessage("Image id must be greater than 0")
                                  .Must(x => context.Images.Any(i => i.Id == x)).WithMessage("Image with ID: {PropertyValue} doesn't exist").When(x => x.ImageId.HasValue);*/
       
        }
      
    }


}
