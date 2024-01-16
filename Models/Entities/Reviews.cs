using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BookLibrary.Models.Entities
{
    public class Reviews : BaseEntity
    {
        [Required]
        public string Title { get; set; }

        [Required]
        public string Text { get; set; }
        [Required]
        public int BookId { get; set; }
        [Required]
        public string Person { get; set; }

        [ForeignKey("BookId")]
        [ValidateNever]
        public Book? Book { get; set; }
    }
}
