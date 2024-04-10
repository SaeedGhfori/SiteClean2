using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Site.Persistence.Seeds.IdentitySeed
{
    public class IdentityDbContextSeed
    {
        public static void SeedData(ModelBuilder modelBuilder)
        {
            foreach (var user in GetUsers())
            {
                modelBuilder.Entity<ApplicationUser>().HasData(user);
            }
            foreach (var identityRole in GetIdentityRoles())
            {
                modelBuilder.Entity<IdentityRole>().HasData(identityRole);
            }
            foreach (var userRole in GetUserRoles())
            {
                modelBuilder.Entity<IdentityUserRole<string>>().HasData(userRole);
            }
        }

        #region ApplicationUser

        private static IEnumerable<ApplicationUser> GetUsers()
        {
            var hasher = new PasswordHasher<ApplicationUser>();

            return new List<ApplicationUser>()
            {
                new ApplicationUser
                {
                    Id = "05446344-f9cc-4566-bd2c-36791b4e28ed",
                    Email = "admin@localhost.com",
                    NormalizedEmail = "ADMIN@LOCALHOST.COM",
                    FirstName = "Admin",
                    LastName = "Adminian",
                    UserName = "admin@localhost.com",
                    NationalCode = "1900123456",
                    NormalizedUserName = "ADMIN@LOCALHOST.COM",
                    PasswordHash = hasher.HashPassword(new ApplicationUser(), "P@ssword1"),
                    EmailConfirmed = true,
                },
                 new ApplicationUser
                 {
                     Id = "2ec9f480-7288-4d0f-a1cd-53cc89968b45",
                     Email = "user@localhost.com",
                     NormalizedEmail = "USER@LOCALHOST.COM",
                     FirstName = "System",
                     LastName = "User",
                     UserName = "user@localhost.com",
                     NationalCode = "1900123457",
                     NormalizedUserName = "USER@LOCALHOST.COM",
                     PasswordHash = hasher.HashPassword(new ApplicationUser(), "P@ssword1"),
                     EmailConfirmed = true,
                 }
            };
        }

        #endregion

        #region IdentityRole

        private static IEnumerable<IdentityRole> GetIdentityRoles()
        {
            return new List<IdentityRole>()
            {
                new IdentityRole
                {
                    Id = "9845f909-799c-45fd-9158-58c1336ffddc",
                    Name = "Employee",
                    NormalizedName = "EMPLOYEE"
                },
                new IdentityRole
                {
                    Id = "cb275765-1cac-4652-a03f-f8871dd575d1",
                    Name = "Administrator",
                    NormalizedName = "ADMINISTRATOR"
                }
            };
        }


        #endregion

        #region IdentityUserRole

        private static IEnumerable<IdentityUserRole<string>> GetUserRoles()
        {
            return new List<IdentityUserRole<string>>()
            {
                  new IdentityUserRole<string>
                  {
                      UserId = "05446344-f9cc-4566-bd2c-36791b4e28ed",
                      RoleId = "cb275765-1cac-4652-a03f-f8871dd575d1"
                  },
                  new IdentityUserRole<string>
                  {
                      UserId = "2ec9f480-7288-4d0f-a1cd-53cc89968b45",
                      RoleId = "9845f909-799c-45fd-9158-58c1336ffddc"
                  }
            };
        }


        #endregion



    }
}
