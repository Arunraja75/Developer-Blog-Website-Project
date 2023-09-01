using System.ComponentModel.DataAnnotations;

namespace ArunBlog.Web.Models.Domain.ViewModel
{
    public class AddTagRequest
    {
        [Required]
        public string Name { get; set; }
        [Required]
        public string DisplayName { get; set; }
    }
}
