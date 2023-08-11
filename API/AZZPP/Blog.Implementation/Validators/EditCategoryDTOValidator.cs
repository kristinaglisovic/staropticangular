using Blog.Application.UseCases.DTO.Category;
using Blog.DataAccess;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog.Implementation.Validators
{
    public class EditCategoryDTOValidator : AbstractValidator<UpdateCategoryDTO>
    {

        private BlogDbContext _context;

        public EditCategoryDTOValidator(BlogDbContext context)
        {
            _context = context;

            RuleFor(x => x.Name).Cascade(CascadeMode.Stop).NotEmpty().WithMessage("Name is required.")
                              .MinimumLength(3).WithMessage("Name must have at least 3 characters.");

            RuleFor(x => x.Description).Cascade(CascadeMode.Stop).NotEmpty().WithMessage("Description is required.")
                       .MinimumLength(3).WithMessage("Description must have at least 3 characters.");



            RuleFor(x => x.NameEng).Cascade(CascadeMode.Stop).NotEmpty().WithMessage("Name is required.")
                              .MinimumLength(3).WithMessage("Name must have at least 3 characters.");


            RuleFor(x => x.DescriptionEng).Cascade(CascadeMode.Stop).NotEmpty().WithMessage("Description is required.")
                       .MinimumLength(3).WithMessage("Description must have at least 3 characters.");
               
        }



    }
}
