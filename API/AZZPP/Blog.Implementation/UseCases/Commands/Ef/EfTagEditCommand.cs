using Blog.Application.Exeptions;
using Blog.Application.UseCases.Commands;
using Blog.Application.UseCases.DTO.Tag;
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
    public class EfTagEditCommand : EfUseCase, ITagEditCommand
    {
        private EditTagDTOValidator _validator;
        public EfTagEditCommand(BlogDbContext context, EditTagDTOValidator validator) : base(context)
        {
            _validator = validator;
        }

        public int Id => 67;

        public string Name => "Edit tag";

        public string Description => "Ef edit tag";

        public void Execute(UpdateTagDTO request)
        {
            
            _validator.ValidateAndThrow(request);

            var tag = Context.Tags.Include(t => t.TagTranslation).FirstOrDefault(x=>x.Id==request.TagId);
            var tagTranslation = tag.TagTranslation.NameEng;


            if (tag == null)
            {
                throw new EntityNotFoundException(nameof(Tag), tag.Id);
            }

            if (request.Name!=null)
            {
                tag.Name = request.Name;
            }
            if (request.NameUs != null)
            {
                tag.TagTranslation.NameEng = request.NameUs;
            }

            tag.UpdatedAt = DateTime.UtcNow;


            Context.SaveChanges();
        }
    }
}
