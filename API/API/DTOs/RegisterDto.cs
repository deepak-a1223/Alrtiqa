using System.ComponentModel.DataAnnotations;

namespace API.DTOs
{
    public class RegisterDto
    {
        [Required]
        public string UserName { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string? SurName { get; set; }
        [Required]
        public string RoleName { get; set; }
        [Required]
        [StringLength(16, MinimumLength = 4, ErrorMessage = "Password should be minimum 4 characters and max 16 characters")]
        public string Password { get; set; }
    }
}
