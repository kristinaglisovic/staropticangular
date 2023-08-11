using Blog.Application.UseCases.DTO.Post;
using Blog.DataAccess;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog.Implementation.Validators
{
    public class EditPostDTOValidator:AbstractValidator<EditPostDTO>
    {
        private BlogDbContext _context;
        public EditPostDTOValidator(BlogDbContext context)
        {
            _context = context;
            RuleFor(x => x.Title).Cascade(CascadeMode.Stop).NotEmpty().WithMessage("Title is required.")
                                 .MinimumLength(3).WithMessage("Title must have at least 3 characters.");



            RuleFor(x => x.TitleEng).Cascade(CascadeMode.Stop).NotEmpty().WithMessage("Title eng is required.")
                                 .MinimumLength(3).WithMessage("Title entg must have at least 3 characters.");
                                 


            RuleFor(x => x.Description).Cascade(CascadeMode.Stop).NotEmpty().WithMessage("Description is required")
                                 .MinimumLength(5).WithMessage("Description must have at least 5 characters.");

            RuleFor(x => x.DescriptionEng).Cascade(CascadeMode.Stop).NotEmpty().WithMessage("Description eng is required")
                                 .MinimumLength(5).WithMessage("Description must have at least 5 characters.");


            RuleFor(x => x.UserId).Cascade(CascadeMode.Stop).NotEmpty().WithMessage("User id is required.").GreaterThan(0).WithMessage("User id must be greater than 0")
                                  .Must(x => context.Users.Any(c => c.Id == x)).WithMessage("User with ID: {PropertyValue} doesn't exist");
            //ako je vrednost nullable When(x=>x.UserId.HasValue)
            //[]
            RuleFor(x => x.CategoriesIds).Cascade(CascadeMode.Stop)
              .NotEmpty().WithMessage("CategoryIds are required.")
              .Must(x => x.Distinct().Count() == x.Count()).WithMessage("Duplicate values are not allowed.")
              .DependentRules(() =>
              {
                  RuleForEach(x => x.CategoriesIds).NotEmpty().WithMessage("Value should not be lower than 0 or 0.");
                  RuleForEach(x => x.CategoriesIds).Must(x => context.Categories.Any(y => y.Id == x && y.IsActive)).WithMessage("There are categories that doesnt exist");
              });
            RuleFor(x => x.TagsIds).Cascade(CascadeMode.Stop)
            .NotEmpty().WithMessage("TagsIds are required.")
            .Must(x => x.Distinct().Count() == x.Count()).WithMessage("Duplicate values are not allowed.")
            .DependentRules(() =>
            {
                RuleForEach(x => x.TagsIds).NotEmpty().WithMessage("Value should not be lower than 0 or 0.");
                RuleForEach(x => x.TagsIds).Must(x => context.Tags.Any(y => y.Id == x && y.IsActive)).WithMessage("There are tags that doesnt exist");
            });
            RuleFor(x => x.ImagesIds).Cascade(CascadeMode.Stop)
            .NotEmpty().WithMessage("ImageIds are required.")
            .Must(x => x.Distinct().Count() == x.Count()).WithMessage("Duplicate ids are not allowed.")
            .DependentRules(() =>
            {
                RuleForEach(x => x.ImagesIds).NotEmpty().WithMessage("Value should not be lower than 0 or 0.");
                RuleForEach(x => x.ImagesIds).Must(x => context.Images.Any(y => y.Id == x && y.IsActive)).WithMessage("There are images that doesnt exist");
                RuleForEach(x => x.ImagesIds).Must(z => !context.Users.Any(y => y.ImageId == z)).WithMessage("There are images that are used by user");
            });

            RuleFor(x => x.Heading1).Cascade(CascadeMode.Stop).NotEmpty().WithMessage("Heading1 is required.")
                                 .MinimumLength(3).WithMessage("Heading1 must have at least 3 characters.");


            RuleFor(x => x.Heading1Eng).Cascade(CascadeMode.Stop).NotEmpty().WithMessage("Heading1 eng is required.")
                                .MinimumLength(3).WithMessage("Heading1 eng must have at least 3 characters.");


            RuleFor(x => x.Text1).Cascade(CascadeMode.Stop).NotEmpty().WithMessage("Tekst 1 ne sme biti prazan.").MinimumLength(3).WithMessage("Tekst 1 mora imati najmanje 3 karaktera.");

            RuleFor(x => x.Text1Eng).Cascade(CascadeMode.Stop).NotEmpty().WithMessage("Text 1 eng is required.").MinimumLength(3).WithMessage("Text 1 eng must have at least 3 characters.");


        }

    }
}