using BookLibrary.Data;
using BookLibrary.Models;
using BookLibrary.Models.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace BookLibrary.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly BookLibraryDbContext _dbContext;

        public HomeController(ILogger<HomeController> logger, BookLibraryDbContext _db)
        {
            _logger = logger;
            _dbContext = _db;
        }

        public IActionResult Index()
        {
            //ViewData["UserID"] = _userManager.GetUserId(this.User);
            var result = _dbContext.Books.ToList();
            var book = new IndexVM
            {
                Books = result
            };

            return View(book);
        }

        [Authorize]
        public IActionResult Details(int id)
        {
            //if (Id == null)
            //{
            //    return NotFound();
            //}

            var booker = _dbContext.Books.Find(id);
            var bookDetails = new DetailsVM
            {
                Title = booker.Title,
                Author = booker.Author,
                AvailableCopies = booker.AvailableCopies,
                Category = booker.Category,
                CopiesInLibrary = booker.CopiesInLibrary,
                CopiesOutLibrary = booker.CopiesOutLibrary,
                EVersion = booker.EVersion,
                GenreType = booker.GenreType,
                CoverImageUrl = booker.CoverImageUrl,
                Pages = booker.Pages,
                Publisher = booker.Publisher,
                Language = booker.Language,
                Description = booker.Description,
                ISBN = booker.ISBN
            };

            return View(bookDetails);

        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}