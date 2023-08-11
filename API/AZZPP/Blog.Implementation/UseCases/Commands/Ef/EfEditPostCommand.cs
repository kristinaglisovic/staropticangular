using Blog.Application.Exeptions;
using Blog.Application.UseCases.Commands;
using Blog.Application.UseCases.DTO.Post;
using Blog.DataAccess;
using Blog.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog.Implementation.UseCases.Commands.Ef
{
    public class EfEditPostCommand : EfUseCase, IEditPostCommand
    {
        public EfEditPostCommand(BlogDbContext context) : base(context)
        {
        }

        public int Id => 70;

        public string Name => "Edit post";

        public string Description => "Ef edit post";

        public void Execute(EditPostDTO request)
        {
            var post= Context.Posts.Include(x=>x.Categories).Include(x=>x.Tags).Include(x=>x.Images).Include(x=>x.PostTranslation).FirstOrDefault(x=>x.Id==request.PostId);
            var updated = false;

            if (post == null)
            {
                throw new EntityNotFoundException(nameof(Post), post.Id);
            }

            if (post.Title != request.Title && !Context.Posts.Any(x=>x.Title.Equals(request.Title)))
            {
                post.Title = request.Title;
                updated = true;
            }


            else if (post.Title != request.Title && Context.Posts.Any(x => x.Title.Equals(request.Title)))
            {
                throw new EditValidationException($"Već postoji post sa srpskim naslovom '{request.Title}'","TitleSr");

            }

            if (post.PostTranslation.TitleEng != request.TitleEng && !Context.Posts.Any(x => x.PostTranslation.TitleEng.Equals(request.TitleEng)))
            {
                post.PostTranslation.TitleEng = request.TitleEng;
                updated = true;
            }
            else if(post.PostTranslation.TitleEng != request.TitleEng && Context.Posts.Any(x => x.PostTranslation.TitleEng.Equals(request.TitleEng)))
            {
                throw new EditValidationException($"Već postoji post sa engleskim naslovom '{request.TitleEng}'", "TitleEng");
            }


            if (post.Description != request.Description)
            {
                post.Description = request.Description;
                updated=true;
            }

            if (post.PostTranslation.DescriptionEng != request.DescriptionEng)
            {
                post.PostTranslation.DescriptionEng = request.DescriptionEng;
                updated = true;
            }

            if (post.Heading1 != request.Heading1)
            {
                post.Heading1 = request.Heading1;
                updated = true;
            }

            if (post.PostTranslation.Heading1Eng != request.Heading1Eng)
            {
                post.PostTranslation.Heading1Eng = request.Heading1Eng;
                updated = true;
            }

            if (post.Heading2 != request.Heading2)
            {
                post.Heading2 = request.Heading2;
                updated = true;
            }
            if (post.PostTranslation.Heading2Eng!= request.Heading2Eng)
            {
                post.PostTranslation.Heading2Eng = request.Heading2Eng;
                updated = true;
            }

            if (post.Text1 != request.Text1)
            {
                post.Text1= request.Text1;
                updated = true;
            }

            if (post.PostTranslation.Text1Eng != request.Text1Eng)
            {
                post.PostTranslation.Text1Eng  = request.Text1Eng;
                updated = true;
            }

            if (post.Text2 != request.Text2)
            {
                post.Text2 = request.Text2;
                updated = true;
            }

            if (post.PostTranslation.Text2Eng != request.Text2Eng)
            {
                post.PostTranslation.Text2Eng = request.Text2Eng;
                updated = true;
            }


            var postTags = post.Tags.Select(x=>x.TagId).ToList();
            var postCategories = post.Categories.Select(x=>x.CategoryId).ToList();
            var postImages = post.Images.Select(x => x.ImageId).ToList();

            

            if (!request.CategoriesIds.SequenceEqual(postCategories))
            {
                Context.PostCategories.RemoveRange(post.Categories);

                post.Categories = request.CategoriesIds.Select(x => new PostCategory
                {
                    CategoryId = x
                }).ToList();

                updated = true;
            }


            

            if (!request.TagsIds.SequenceEqual(postTags))
            {
                Context.PostTags.RemoveRange(post.Tags);

                post.Tags = request.TagsIds.Select(x => new PostTag
                {
                    TagId = x
                }).ToList();

                updated = true;
            }

            if (!request.ImagesIds.SequenceEqual(postCategories))
            {
                Context.PostImages.RemoveRange(post.Images);

                post.Images = request.ImagesIds.Select(x => new PostImage
                {
                    ImageId = x
                }).ToList();

                updated = true;
            }


            if (updated)
            {
                post.UpdatedAt = DateTime.Now;
                Context.SaveChanges();
            }


        }
    }
}
