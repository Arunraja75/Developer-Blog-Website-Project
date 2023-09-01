﻿using ArunBlog.Web.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace ArunBlog.Web.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly AuthDbContext authDbContext;

        public UserRepository(AuthDbContext authDbContext)
        {
            this.authDbContext = authDbContext;
        }
        public async Task<IEnumerable<IdentityUser>> GetAll()
        {
           var users =  await authDbContext.Users.ToListAsync();

            var superAdminUser = await authDbContext.Users.FirstOrDefaultAsync(x => x.Email == "SuperAdmin@blog.com");
            if (superAdminUser != null)
            {
                users.Remove(superAdminUser);

            }
            return users;
        }
    }
}
