using System.ComponentModel.DataAnnotations;

namespace Site.Application.Definitions.Dtos.Identity
{
    public class ChangePasswordRequest
    {
        [Required]
        [MinLength(6)]
        public string UserName { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string OldPassword { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [MinLength(6)]
        public string NewPassword { get; set; }
    }

}
