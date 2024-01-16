using BookLibrary.Models.Entities;
using System.ComponentModel.DataAnnotations;

namespace BookLibrary.Models.ViewModels
{
    public class DetailsVM
    {
        [Required]
        public string Title { get; set; }
        [Required]
        public string Description { get; set; }
        [Required]
        public string Author { get; set; }
        [Required]
        public string Publisher { get; set; }
        public string Language { get; set; }
        [Required]
        public string ISBN { get; set; }
        public int Pages { get; set; }
        public DateTime LibraryAddDate { get; set; }
        public DateTime PublishedDate { get; set; }
        public int CopiesInLibrary { get; set; }
        public int CopiesOutLibrary { get; set; }
        public int AvailableCopies { get; set; }
        public string EVersion { get; set; }
        public string GenreType { get; set; }
        public string Category { get; set; }
        public string CoverImage { get; set; }
        public string CoverImageUrl { get; set; }
        public Genre? Genre { get; set; }
    }
}
