using Blog.Application.UseCases.DTO;
using System;
using System.Collections.Generic;

namespace Blog.Application.UseCases.DTO.Post
{
    public class PostDTO : BaseDTO
    { 
        public string Title { get; set; }
        public string TitleSr { get; set; }
        public string TitleEng { get; set; }
        public string Description { get; set; }
        public string DescriptionSr { get; set; }
        public string DescriptionEng { get; set; }
        public string Heading1 { get; set; }
        public string Heading1Sr { get; set; }
        public string Heading1Eng { get; set; }
        public string Heading2 { get; set; }
        public string Heading2Sr { get; set; }
        public string Heading2Eng { get; set; }
        public string Text1 { get; set; }
        public string Text1Sr { get; set; }
        public string Text1Eng { get; set; }
        public string Text2 { get; set; }
        public string Text2Sr { get; set; }
        public string Text2Eng { get; set; }
        public IEnumerable<int> TagsIds { get; set; }
        public IEnumerable<int> CategoriesIds { get; set; }
        public IEnumerable<int> ImagesIds { get; set; }
        public string Author { get; set; }
        public string AuthorRole { get; set; }

        public IEnumerable<CommentDto> Comments { get; set; }
        public IEnumerable<string> Images { get; set; }
        public IEnumerable<string> Tags { get; set; }
        public IEnumerable<string> Categories { get; set; }

        public int Likes { get; set; }

        public IEnumerable<LikeDto> LikesInfo { get; set; }
        public int CommentsCount { get; set; }

        public string CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public bool IsActive { get; set; }

        public int ImagesCount { get; set; }
    }

    public class CommentDto:BaseDTO
    {
        public string Comment { get; set; }
        public string User { get; set; }
        public DateTime CommentedAt { get; set; }

        public string Image { get; set; }
    }


    public class LikeDto : BaseDTO
    {
        public string Username { get; set; }
        public int UserId { get; set; }
    }
}
