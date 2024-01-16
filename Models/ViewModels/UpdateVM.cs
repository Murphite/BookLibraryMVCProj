using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace BookLibrary.Models.ViewModels
{
    public class UpdateVM
    {
        public int Id { get; set; }
        [Required]
        [Display(Name = "Book Title")]
        public string? Title { get; set; }
        public string? Description { get; set; }
        [Required]
        [Display(Name = "Book Author:")]
        public string? Author { get; set; }
        public string? Publisher { get; set; }
        public string? Language { get; set; }
        public string? ISBN { get; set; }
        [Display(Name = "Total pages of book:")]
        public int Pages { get; set; }
        public DateTime LibraryAddDate { get; set; }
        [Display(Name = "Published Date:")]
        public DateTime PublishedDate { get; set; }
        [Display(Name = "Copies In Library:")]
        public int CopiesInLibrary { get; set; }
        [Display(Name = "Copies Out of Library:")]
        public int CopiesOutLibrary { get; set; }
        [Display(Name = "Available Copies:")]
        public int AvailableCopies { get; set; }
        public string? EVersion { get; set; }
        public string? GenreType { get; set; }
        public string? Category { get; set; }
        [Display(Name = "Upload Book Cover Photo:")]
        public string CoverImage { get; set; }
        public string? CoverImageUrl { get; set; }
       
    }
}
