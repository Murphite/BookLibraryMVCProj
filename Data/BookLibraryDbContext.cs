using BookLibrary.Models;
using BookLibrary.Models.Entities;
using BookLibrarySoln.Models.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace BookLibrary.Data
{
    public class BookLibraryDbContext : IdentityDbContext<AppUser>
    {
        public BookLibraryDbContext(DbContextOptions<BookLibraryDbContext> options):base(options)
        {
        
        }

        public DbSet<Book> Books { get; set; }

        //public DbSet<Genre> Genres { get; set; }
        //public DbSet<Reviews> Reviews { get; set; }
        //public DbSet<SubCategories> SubCategories { get; set; }
       // public DbSet<AppUser> AppUsers { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Book>().HasData(
                new Book
                {
                    Id = 1,
                    Title = "Harry Potter and the Prisoner of Azkaban",
                    Description = "Magic",
                    Author = "J.K Rowling",
                    Pages = 780,
                    Publisher = "Bloomsbury",
                    Language = "English",
                    ISBN = "975609876112",
                    LibraryAddDate = DateTime.Now,
                    CopiesInLibrary = 50,
                    CopiesOutLibrary = 3,
                    AvailableCopies = 47,
                    CreatedAt = DateTime.Now,
                    UpdatedAt = DateTime.Now,
                    DeletedAt = null,
                    EVersion = "Yes",
                    Category = "Non-Fiction",
                    GenreType = "Drama",
                    CoverImageUrl = "https://th.bing.com/th/id/OIP.l4VuVl26t-3FF6VNWWluKQHaLH?rs=1&pid=ImgDetMain",
                });

            modelBuilder.Entity<Genre>().HasData(
                new Genre
                {
                    Id = 1,
                    Name = "Fiction Books",  
                },
                new Genre
                {
                    Id = 2,
                    Name = "Non-Fiction Books",
                },
                new Genre
                {
                    Id = 3,
                    Name = "CD Genres"
                }
                
                );
        }

    }
}
