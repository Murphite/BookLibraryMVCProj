using System.ComponentModel.DataAnnotations;

namespace BookLibrarySoln.Models.ViewModels
{
    public class UserToRegisterVM
    {
        [Required]
        [StringLength(15, MinimumLength = 2, ErrorMessage = "2 - 15 character allowed")]
        public string FirstName { get; set; }
        [Required]
        [StringLength(15, MinimumLength = 2, ErrorMessage = "2 - 15 character allowed")]
        public string LastName { get; set; }
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        public string PhoneNumber { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Confirm Password")]
        [Compare("Password")]
        public string ConfirmPassword { get; set; }

    }
}
