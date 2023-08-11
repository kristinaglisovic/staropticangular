using System.Collections.Generic;

namespace Blog.Application.UseCases.DTO.Searches
{
    public class BaseSearch
    {
        public string Keyword { get; set; }
    }


    public class SingleTranslatedSearch
    {
     
        public int id { get; set; }
        public bool? eng { get; set; }

        public SingleTranslatedSearch(int id, bool? eng)
        {
            this.id = id;
            this.eng = eng;
        }

    }



    public class PagedSearch
    {
        public int? PerPage { get; set; } = 4;
        public int? Page { get; set; } = 1;
    }

    public class BasePagedSearch : PagedSearch
    {
        public string Keyword { get; set; }
        public bool? Eng { get; set; }
    }

    public class PostPagedSearch : BasePagedSearch {
        public bool? HasComments { get; set; }
        public bool? HasLikes { get; set; }

        public bool? IsActive { get; set; }
        public bool? Recent { get; set; }

     

        public IEnumerable<int> CategoryIds { get; set; }
        public IEnumerable<int> TagsIds { get; set; }

        //za filter
        public IEnumerable<int> LikeIds { get; set; }

        public PostsOrder? SortOrder { get; set; }

    }

    public enum PostsOrder
    {
        LikesAsc,
        LikesDesc,
        CommentsAsc,
        CommentsDesc,
    }

    public class ImagePagedSearch : BasePagedSearch
    {
        public bool? PostsOnly { get; set; }
        public bool? IsActive { get; set; }
    }

    public class TagPagedSearch : BasePagedSearch
    {
        public bool? HasPosts { get; set; }

        public bool? IsPostActive { get; set; }
        public bool? IsActive { get; set; }
    }


    public class CategoriesPagedSearch : BasePagedSearch
    {
        public bool? HasPosts { get; set; }
        public bool? IsActive { get; set; }
    }

    public class LikePagedSearch : BasePagedSearch
    { 
    }

    public class CommentsPagedSearch : BasePagedSearch
    {

    }

    public class UsersPagedSearch : BasePagedSearch
    {
        public bool? HasPosts { get; set; }
        public bool? HasLikes { get; set; }
        public bool? HasComments { get; set; }
        public bool? HasImage { get; set; }
    }
    public class PostTagsPagedSearch : BasePagedSearch
    { 
    }  
    
    public class PostImagesPagedSearch : BasePagedSearch
    { 
    }public class PostCategoryPagedSearch : BasePagedSearch
    { 
    }



}
