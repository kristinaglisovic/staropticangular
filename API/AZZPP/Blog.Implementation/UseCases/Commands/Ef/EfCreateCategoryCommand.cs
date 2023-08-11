using Blog.Application.UseCases.Commands;
using Blog.Application.UseCases.DTO.Category;
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
    public class EfCreateCategoryCommand : EfUseCase, ICreateCategoryCommand
    {
        private CreateCategoryDTOValidator _validator;
        public EfCreateCategoryCommand(BlogDbContext context, CreateCategoryDTOValidator validator) : base(context)
        {
            _validator = validator;
        }

        public int Id => 27;

        public string Name => "Create category";

        public string Description => "Create Category using EF";

        public void Execute(CreateCategoryDTO request)
        {
            _validator.ValidateAndThrow(request);
            var newC = new Category
            {
                Name = request.Name,
                Desciption=request.Description
            };

            var translate = new CategoryTranslation
            {
                NameEng = request.NameEng,
                DesciptionEng=request.DescriptionEng
            };
            newC.CategoryTranslation= translate;




            Context.Categories.Add(newC);
            Context.SaveChanges();
        }
    }
}
