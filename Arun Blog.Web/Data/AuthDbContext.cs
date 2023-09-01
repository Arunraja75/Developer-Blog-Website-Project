using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace ArunBlog.Web.Data
{
    public class AuthDbContext : IdentityDbContext
    {
        public AuthDbContext(DbContextOptions<AuthDbContext> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            //seed roles (user, admin, superadmin)
            var adminRoleId = "bb9cf104-1e1d-48fe-8eea-43abc6bcd979";
            var superAdminRoleId = "a8f1f39e-6fd7-4ea2-9dc4-000b504e5798";
            var userRoleId = "ec664642-44f3-4830-86f6-014151509a61";

            var roles = new List<IdentityRole>
            {
                new IdentityRole
                {
                    Name= "Admin",
                    NormalizedName = "Admin",
                    Id = adminRoleId,
                    ConcurrencyStamp = adminRoleId
                },
                new IdentityRole
                {
                    Name = "SuperAdmin",
                    NormalizedName = "SuperAdmin",
                    Id = superAdminRoleId,
                    ConcurrencyStamp = superAdminRoleId
                },
                new IdentityRole
                {
                    Name = "User",
                    NormalizedName = "User",
                    Id = userRoleId,
                    ConcurrencyStamp = userRoleId
                }
            };
            builder.Entity<IdentityRole>().HasData(roles);
            //seed superadmin
            var superAdminId = "47d8475e-06ba-4c00-99c8-d73306422e1c";
            var superAdminUser = new IdentityUser
            {
                UserName = "SuperAdmin@blog.com",
                Email = "SuperAdmin@blog.com",
                NormalizedEmail = "SuperAdmin@blog.com".ToUpper(),
                NormalizedUserName = "SuperAdmin@blog.com".ToUpper(),
                Id = superAdminId

            };
            superAdminUser.PasswordHash = new PasswordHasher<IdentityUser>().HashPassword(superAdminUser, "Arunsuperadmin71@");
            builder.Entity<IdentityUser>().HasData(superAdminUser);
            //add all roles to superadmin

            var superAdminRoles = new List<IdentityUserRole<string>>
            {
                new IdentityUserRole<string>
                {
                    RoleId = adminRoleId,
                    UserId = superAdminId
                },
                new IdentityUserRole<string>
                {
                    RoleId = superAdminRoleId,
                    UserId = superAdminId
                },
                new IdentityUserRole<string>
                {
                    RoleId = userRoleId,
                    UserId = superAdminId
                }
            };
            builder.Entity<IdentityUserRole<string>>().HasData(superAdminRoles);
        }
    }
}
