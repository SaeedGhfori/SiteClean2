using Microsoft.AspNetCore.Identity;

namespace Site.Domain.Entities.Users
{
    public class ApplicationUser : IdentityUser
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string NationalCode { get; set; }
    }
}
