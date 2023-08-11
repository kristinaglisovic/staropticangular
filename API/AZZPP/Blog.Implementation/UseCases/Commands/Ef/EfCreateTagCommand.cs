using Blog.Application.UseCases.Commands;
using Blog.Application.UseCases.DTO.Tag;
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
    public class EfCreateTagCommand : EfUseCase, ICreateTagCommand
    {
        private CreateTagDTOValidator _validator;
        public EfCreateTagCommand(BlogDbContext context, CreateTagDTOValidator validator) : base(context)
        {
            _validator = validator;
        }

        public int Id => 20;

        public string Name => "Create tag";

        public string Description => "Ef create tag";

        public void Execute(CreateTagDTO request)
        {
            _validator.ValidateAndThrow(request);
            var newTag = new Tag
            {
                Name = request.Name,
            };

            var translate = new TagTranslation
             {
                 NameEng= request.NameUs,
             };
             newTag.TagTranslation = translate;
            
          
            Context.Tags.Add(newTag);
            Context.SaveChanges();
        }
    }
}
