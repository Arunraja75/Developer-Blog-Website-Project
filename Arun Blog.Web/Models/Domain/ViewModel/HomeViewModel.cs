namespace ArunBlog.Web.Models.Domain.ViewModel
{
    public class HomeViewModel
    {
        public IEnumerable<BlogPost> blogPosts { get; set; }

        public IEnumerable<Tag> Tags { get; set; }
    }
}
