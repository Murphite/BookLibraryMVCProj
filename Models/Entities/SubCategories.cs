using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations.Schema;

namespace BookLibrary.Models.Entities
{
    public class SubCategories : BaseEntity
    {
        public string Title { get; set; }
        public int GenreId { get; set; }

        [ForeignKey("GenreId")]
        [ValidateNever]
        public Genre? Genre { get; set; }

    }
}
