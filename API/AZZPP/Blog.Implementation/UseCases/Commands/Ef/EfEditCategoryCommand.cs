using Blog.Application.Exeptions;
using Blog.Application.UseCases.Commands;
using Blog.Application.UseCases.DTO.Category;
using Blog.DataAccess;
using Blog.Domain.Entities;
using Blog.Implementation.Validators;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog.Implementation.UseCases.Commands.Ef
{
    public class EfEditCategoryCommand : EfUseCase, ICategoryEditCommand
    {
        private EditCategoryDTOValidator _validator;
        public EfEditCategoryCommand(BlogDbContext context, EditCategoryDTOValidator validator) : base(context)
        {
            _validator = validator;
        }

        public int Id => 68;

        public string Name =>"Edit a category";

        public string Description => "Ef Edit Category";

        public void Execute(UpdateCategoryDTO request)
        {

            var category = Context.Categories.Include(t => t.CategoryTranslation).FirstOrDefault(x => x.Id == request.CategoryId);
   

            var updated = false;


            _validator.ValidateAndThrow(request);

            if (category== null)
            {
                throw new EntityNotFoundException(nameof(Category), category.Id);
            }


            if (category.Name != request.Name && !Context.Categories.Any(x=>x.Name.Equals(request.Name)))
            {
                category.Name = request.Name;
                updated = true;
            }
            else if(category.Name != request.Name && Context.Categories.Any(x => x.Name.Equals(request.Name)))
            {
                throw new EditValidationException($"Već postoji kategorija sa imenom '{request.Name}'","nameSr");
            }
            if (category.CategoryTranslation.NameEng != request.NameEng && !Context.CategoryTranslations.Any(x => x.NameEng.Equals(request.NameEng)))
            {
                category.CategoryTranslation.NameEng = request.NameEng;
                updated = true;
            }
            else if (category.CategoryTranslation.NameEng != request.NameEng && Context.CategoryTranslations.Any(x => x.NameEng.Equals(request.NameEng)))
            {
                throw new EditValidationException($"Već postoji kategorija sa eng imenom '{request.NameEng}'", "nameUs");
            }



            if (category.Desciption != request.Description)
            {
                category.Desciption= request.Description;
                updated = true;
            }

            if (category.CategoryTranslation.DesciptionEng != request.DescriptionEng)
            {
                 category.CategoryTranslation.DesciptionEng = request.DescriptionEng;
                 updated = true;
            }


            if (updated)
            {
                category.UpdatedAt = DateTime.Now;
                Context.SaveChanges();
            }
         


        }
    }
}
