using System.ComponentModel.DataAnnotations;

namespace Site.Application.Definitions.Dtos.Identity
{
    public class ForgotsPasswordRequest
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }
    }

}
