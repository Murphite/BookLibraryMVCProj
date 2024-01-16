using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BookLibrarySoln.Models.Entities
{
    public class AppUser : IdentityUser
    {
        [Required]
        [Column(TypeName = "nvarchar(100)")]
        public string FirstName { get; set; }

        [Required]
        [Column(TypeName = "nvarchar(100)")]
        public string LastName { get; set; }
    }
}
