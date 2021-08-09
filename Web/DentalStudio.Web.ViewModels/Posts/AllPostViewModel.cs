namespace DentalStudio.Web.ViewModels.Posts
{
    using System.Collections.Generic;

    public class AllPostViewModel
    {
        public IEnumerable<PostViewModel> BlogPosts { get; set; }

        public int CurrentPage { get; set; }

        public int PagesCount { get; set; }

    }
}
