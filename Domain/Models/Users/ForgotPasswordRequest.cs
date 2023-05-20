using System.ComponentModel.DataAnnotations;

namespace Domain.Models.Users
{
    public class ForgotPasswordRequest
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }
    }
}
