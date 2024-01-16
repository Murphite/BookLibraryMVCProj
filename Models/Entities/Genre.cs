using System.ComponentModel.DataAnnotations;

namespace BookLibrary.Models.Entities
{
    public class Genre
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
    }
}
