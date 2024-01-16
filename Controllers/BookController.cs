using BookLibrary.Data;
using BookLibrary.Models.Entities;
using BookLibrary.Models.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BookLibrary.Controllers
{
    [Authorize(Roles = "admin")]
    public class BookController : Controller
    {
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly BookLibraryDbContext _db;


        public BookController(IWebHostEnvironment webHostEnvironment,
            BookLibraryDbContext db)
        {
            _webHostEnvironment = webHostEnvironment;
            _db = db;
        }

        public async Task<IActionResult> Services()
        {

            List<Book> objBookList = _db.Books.ToList();
            var listBook = new IndexVM
            {
                Books = objBookList
            };
            return View(listBook);
        }

        public async Task<IActionResult> AddNewBook()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> AddNewBook(Book book)
        {
            if (ModelState.IsValid)
            {
                if (book.CoverImage != null)
                {
                    string folder = "images/cover/";
                    folder += Guid.NewGuid().ToString() + "_" + book.CoverImage.FileName;

                    book.CoverImageUrl = folder;
                    string serverFolder = Path.Combine(_webHostEnvironment.WebRootPath, folder);
                    await book.CoverImage.CopyToAsync(new FileStream(serverFolder, FileMode.Create));
                }

                Console.WriteLine(book.CoverImage);

                _db.Add(book);
                await _db.SaveChangesAsync();

                return RedirectToAction("Index", "Home");
            }
            return View();
        }

        //DELETE
        public IActionResult Delete(int Id)
        {
            var bookToDelete = _db.Books.Find(Id);

            if (bookToDelete != null)
            {
                // Mark the entity for deletion
                _db.Books.Remove(bookToDelete);

                // Save changes to the database
                _db.SaveChanges();
            }

            return RedirectToAction("Services", "Book");
        }


        //EDIT
        public async Task<IActionResult> Edit(int id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            Book? bookFromDb = _db.Books.FirstOrDefault(x => x.Id == id);
            //Book? bookFromDb1 = _db.Books.Find(id);
            //Book? bookFromDb2 = _db.Books.Where(x => x.Id == id).FirstOrDefault();

            if (bookFromDb == null)
            {
                return NotFound();
            }
            return View(bookFromDb);
        }
        [HttpPost]
        public async Task<IActionResult> Edit(Book model)
        {
            var dataToUpdate = _db.Books.Find(model.Id);
            if (model.CoverImage != null)
            {

                string folder1 = dataToUpdate.CoverImageUrl;
                string serverFolder1 = Path.Combine(_webHostEnvironment.WebRootPath, folder1);
                System.IO.File.Delete(serverFolder1);

                string folder = "images/cover/";
                folder += Guid.NewGuid().ToString() + "_" + model.CoverImage.FileName;
                model.CoverImageUrl = folder;
                string serverFolder = Path.Combine(_webHostEnvironment.WebRootPath, folder);
                await model.CoverImage.CopyToAsync(new FileStream(serverFolder, FileMode.Create));
            }
            if (dataToUpdate != null)
            {

                dataToUpdate.Title = model.Title;
                dataToUpdate.Description = model.Description;
                dataToUpdate.Author = model.Author;
                dataToUpdate.Publisher = model.Publisher;
                dataToUpdate.Category = model.Category;
                dataToUpdate.ISBN = model.ISBN;
                dataToUpdate.Pages = model.Pages;
                dataToUpdate.Language = model.Language;
                dataToUpdate.GenreType = model.GenreType;
                dataToUpdate.EVersion = model.EVersion;
                dataToUpdate.CoverImageUrl = model.CoverImageUrl;
                dataToUpdate.CopiesInLibrary = model.CopiesInLibrary;
                dataToUpdate.CopiesOutLibrary = model.CopiesOutLibrary;
                dataToUpdate.AvailableCopies = model.AvailableCopies;




                var result = _db.Update(dataToUpdate);
                await _db.SaveChangesAsync();
                if (result != null)
                {
                    return RedirectToAction("Index", "Home");

                }
                return View(model);

            }
            return View(model);
        }



        //public async Task<IActionResult> Edit(Book model)
        //{
        //    var dataToUpdate = _db.Books.Find(model.Id);

        //    if (dataToUpdate == null)
        //    {
        //        // Handle the case when the book is not found
        //        return NotFound();
        //    }

        //    if (model.CoverImage != null)
        //    {
        //        string folder1 = dataToUpdate.CoverImageUrl;
        //        string serverFolder1 = Path.Combine(_webHostEnvironment.WebRootPath, folder1);

        //        if (System.IO.File.Exists(serverFolder1))
        //        {
        //            System.IO.File.Delete(serverFolder1);
        //        }

        //        string folder = "images/cover/";
        //        folder += Guid.NewGuid().ToString() + "_" + model.CoverImage.FileName;
        //        model.CoverImageUrl = folder;
        //        string serverFolder = Path.Combine(_webHostEnvironment.WebRootPath, folder);

        //        await model.CoverImage.CopyToAsync(new FileStream(serverFolder, FileMode.Create));
        //    }

        //    dataToUpdate.Title = model.Title;
        //    dataToUpdate.Description = model.Description;
        //    dataToUpdate.Author = model.Author;
        //    dataToUpdate.Publisher = model.Publisher;
        //    dataToUpdate.Category = model.Category;
        //    dataToUpdate.ISBN = model.ISBN;
        //    dataToUpdate.Pages = model.Pages;
        //    dataToUpdate.Language = model.Language;
        //    dataToUpdate.GenreType = model.GenreType;
        //    dataToUpdate.EVersion = model.EVersion;
        //    dataToUpdate.CoverImageUrl = model.CoverImageUrl;
        //    dataToUpdate.CopiesInLibrary = model.CopiesInLibrary;
        //    dataToUpdate.CopiesOutLibrary = model.CopiesOutLibrary;
        //    dataToUpdate.AvailableCopies = model.AvailableCopies;

        //    try
        //    {
        //        _db.Update(dataToUpdate);
        //        await _db.SaveChangesAsync();
        //        return RedirectToAction("Index", "Home");
        //    }
        //    catch (Exception)
        //    {
        //        // Log the exception or handle it as appropriate for your application
        //        return View("Error");
        //    }
        //}


        //public async Task<IActionResult> Edit(Book model)
        //{
        //    var dataToUpdate = _db.Books.Find(model.Id);
        //    if (model.CoverImage != null)
        //    {
        //        string folder1 = dataToUpdate.CoverImageUrl;
        //        string serverFolder1 = Path.Combine(_webHostEnvironment.WebRootPath, folder1);
        //        System.IO.File.Delete(serverFolder1);

        //        string folder = "images/cover/";
        //        folder += Guid.NewGuid().ToString() + "_" + model.CoverImage.FileName;
        //        model.CoverImageUrl = folder;
        //        string serverFolder = Path.Combine(_webHostEnvironment.WebRootPath, folder);
        //        await model.CoverImage.CopyToAsync(new FileStream(serverFolder, FileMode.Create));
        //    }
        //    if (dataToUpdate != null)
        //    {

        //        dataToUpdate.Title = model.Title;
        //        dataToUpdate.Description = model.Description;
        //        dataToUpdate.Author = model.Author;
        //        dataToUpdate.Publisher = model.Publisher;
        //        dataToUpdate.Category = model.Category;
        //        dataToUpdate.ISBN = model.ISBN;
        //        dataToUpdate.Pages = model.Pages;
        //        dataToUpdate.Language = model.Language;
        //        dataToUpdate.GenreType = model.GenreType;
        //        dataToUpdate.EVersion = model.EVersion;
        //        dataToUpdate.CoverImageUrl = model.CoverImageUrl;
        //        dataToUpdate.CopiesInLibrary = model.CopiesInLibrary;
        //        dataToUpdate.CopiesOutLibrary = model.CopiesOutLibrary;
        //        dataToUpdate.AvailableCopies = model.AvailableCopies;


        //        var result = _db.Update(dataToUpdate);
        //        await _db.SaveChangesAsync();
        //        if (result != null)
        //        {
        //            return RedirectToAction("Index", "Home");

        //        }
        //        return View(model);

        //    }
        //    return View(model);
        //}
    }
}
