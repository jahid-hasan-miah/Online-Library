using FirstApp.Data;
using FirstApp.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FirstApp.Repository
{
    public class BookRepository
    {
        public readonly BookStoreContext _context = null;

        public BookRepository(BookStoreContext context)
        {
            _context = context;
        }
        public async Task<List<BookModel>> GetAllBooks()
        {
            var books = new List<BookModel>();
            var allbooks = await _context.Books.ToListAsync();
            if(allbooks?.Any()==true)
            {
                foreach(var book in allbooks)
                {
                    books.Add(new BookModel() {

                         Author = book.Author,
                         Category=book.Category,
                         Description=book.Description,
                         Title=book.Title,
                         Id=book.Id,
                         TotalPages=book.TotalPages,
                         Language=book.Language,
                         CoverPhotoUrl = book.CoverPhotoUrl
                    });
                    
                }
            }
            return books;
        }
        public async Task<int> AddNewBook(BookModel model)
        {
            var newBook = new Books()
            {
                Author = model.Author,
                Description = model.Description,
                TotalPages = model.TotalPages.HasValue ? model.TotalPages.Value : 0,
                Title = model.Title,
                Language = model.Language,
                CoverPhotoUrl= model.CoverPhotoUrl
                //CreatedOn=DateTime.UtcNow,
                //UpdatedOn = DateTime.UtcNow
            };
            await _context.Books.AddAsync(newBook);
            await _context.SaveChangesAsync();

            return newBook.Id;
        }
        public async Task<BookModel> GetBookById(int Id)
        {
            var book = await _context.Books.FindAsync(Id);
            if(book!=null)
            {
                var bookDetails = new BookModel()
                {
                    Author = book.Author,
                    Category = book.Category,
                    Description = book.Description,
                    Title = book.Title,
                    Id = book.Id,
                    TotalPages = book.TotalPages,
                    Language = book.Language,
                    CoverPhotoUrl = book.CoverPhotoUrl
                };
                return bookDetails;
            }
            return null;
        }
        public List<BookModel> SearchBooks(string bookName,string authorName)
        {
            return DataSource().Where(X => X.Title == bookName || X.Author == authorName).ToList();
        }  
        private List<BookModel> DataSource()
        {
            return new List<BookModel>() {
                new BookModel() { Id = 1, Title = "mvc", Author = "Skylark", Category="Action",Language="English",Description="This is mvc books",TotalPages=1500},
                new BookModel() { Id = 2, Title = "data structure", Author = "cohen", Category="Fiction",Language="Spanish",Description="This is Data structure books",TotalPages=1600 },
                new BookModel() { Id = 3, Title = "Algo", Author = "leonard", Category="Drama",Language="Dutch",Description="This is Algo books",TotalPages=1700 },
                new BookModel() { Id = 4, Title = "circuit", Author = "cruise", Category="Poetry",Language="Bangla",Description="This is Circuit books",TotalPages=1800 },
                new BookModel() { Id = 5, Title = "physics", Author = "tom", Category="Math",Language="Hindi",Description="This is physics books",TotalPages=3500 },
            };
        }
        
    }
}
