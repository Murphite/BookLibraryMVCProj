using System.ComponentModel.DataAnnotations;

namespace BookLibrarySoln.Models.ViewModels
{
    public class ForgotPasswordVM
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; } = "";
    }
}
