using FirstApp.Models;
using FirstApp.Repository;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace FirstApp.Controllers
{
    public class BookController : Controller
    {
        private readonly BookRepository _bookRepository = null;
        private readonly IWebHostEnvironment _webHostEnvironment;
        public BookController(BookRepository bookRepository, IWebHostEnvironment webHostEnvironment)
        {
            _bookRepository = bookRepository;
            _webHostEnvironment = webHostEnvironment;
        }
        public async Task<ViewResult> GetAllBooks()
        {
            var data =await  _bookRepository.GetAllBooks();
            return View(data);
        }
        public async Task<ViewResult> GetBookById(int id)
        {
            var data =await _bookRepository.GetBookById(id);
            return View(data);
        }

        public ViewResult AddNewBook(bool isSuccess = false, int bookId=0)
        {
            ViewBag.language = new List<string> {"Bangla","English","French"};
            ViewBag.IsSuccess = isSuccess;
            ViewBag.BookId = bookId;
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> AddNewBook(BookModel bookModel)
        {
            if (ModelState.IsValid)
            {
                if(bookModel.CoverPhoto != null)
                {
                    string folder = "book/cover/";
                    folder += Guid.NewGuid() + bookModel.CoverPhoto.FileName;
                    bookModel.CoverPhotoUrl = "/"+folder;
                    string serverfolder = Path.Combine(_webHostEnvironment.WebRootPath, folder);
                    await bookModel.CoverPhoto.CopyToAsync(new FileStream(serverfolder, FileMode.Create));
                }
                int id = await _bookRepository.AddNewBook(bookModel);
                if (id > 0)
                {
                    return RedirectToAction(nameof(AddNewBook), new { isSuccess = true, bookId = id });
                }
            }
            //ViewBag.IsSuccess = false;
            //ViewBag.BookId = 0;
            return View();
        }

        public List<BookModel> SearchBooks(string bookname, string author)
        {
            return _bookRepository.SearchBooks(bookname,author);
        }

    }
}
