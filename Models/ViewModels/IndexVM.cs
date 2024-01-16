using BookLibrary.Models.Entities;

namespace BookLibrary.Models.ViewModels
{
    public class IndexVM
    {
        public int PageNumber { get; set; }
        public List<Book> Books { get; set; }
    }
}
