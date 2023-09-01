namespace ArunBlog.Web.Repositories
{
    public interface ImageRepository
    {
        Task<string> UploadAsync(IFormFile file);
    }
}
