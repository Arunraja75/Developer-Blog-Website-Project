using Microsoft.AspNetCore.Identity;

namespace ArunBlog.Web.Repositories
{
    public interface IUserRepository
    {
        Task<IEnumerable<IdentityUser>> GetAll();
    }
}
