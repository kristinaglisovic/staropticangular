using Blog.Application.UseCases.Commands;
using Blog.Application.UseCases.DTO.Post;
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
    public class EfCreatePostCommand : EfUseCase, ICreatePostCommand
    {
        private CreatePostDTOValidator _validator;
        public EfCreatePostCommand(BlogDbContext context, CreatePostDTOValidator validator) : base(context)
        {
            _validator = validator;
        }

        public int Id => 3;

        public string Name => "Create Post (EF)";

        public string Description => "Create post using entity framework ";

        public void Execute(CreatePostDTO request)
        {

                _validator.ValidateAndThrow(request);    

                var newPost = new Post
                {
                    Title = request.Title,
                    Description = request.Description,
                    UserId = request.UserId,
                    Categories = request.CategoriesIds.Select(x => new PostCategory
                    {
                        CategoryId = x
                    }).ToList(),
                    Tags = request.TagsIds.Select(x => new PostTag
                    {
                        TagId = x
                    }).ToList(),
                    Images = request.ImagesIds.Select(x => new PostImage
                    {
                        ImageId = x
                    }).ToList(),
                    Heading1 = request.Heading1,
                    Heading2= request.Heading2,
                    Text1=request.Text1,
                    Text2 = request.Text2,
                };

                var translate = new PostTranslation
                {
                     TitleEng=request.TitleEng,
                     DescriptionEng=request.DescriptionEng,
                     Heading1Eng=request.Heading1Eng,
                     Heading2Eng=request.Heading2Eng,
                     Text1Eng=request.Text1Eng,
                     Text2Eng=request.Text2Eng,
                };

                newPost.PostTranslation= translate;


                Context.Posts.Add(newPost);
                Context.SaveChanges();

             
        }
    }
}
