using System.ComponentModel.DataAnnotations;

namespace BookLibrarySoln.Models.ViewModels
{
    public class ResetPasswordVM
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; } = "";

        [Required]
        public string Token { get; set; } = "";

        [Required]
        public string NewPassword { get; set; } = "";
    }
}
