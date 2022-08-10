using Bookstore.Web.Services;
using BookStore.Web.Services;
using BookstoreWeb.Services;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace BookstoreWeb.Controllers
{
    public class BooksController : Controller
    {

        private BookCatalogService _bookCatalog;
        private OpenLibraryService _openLibrary;
        private EcontService _econtService;
        public BooksController(BookCatalogService bookCatalog, OpenLibraryService openLibrary, EcontService econtService)
        {
            _bookCatalog = bookCatalog;
            _openLibrary = openLibrary;
            _econtService = econtService;
        }

        public async  Task<IActionResult> Index()
        {
            var books = await _bookCatalog.GetAllBooks();
            return View(books);
        }

        public async Task<IActionResult> Authors()
        {
            var authors = await _bookCatalog.GetAllAuthors();
            return View(authors);
        }

       public async Task<IActionResult> Author(int id)
        {
          var author = await _bookCatalog.GetAuthor(id);
          return View(author);
       }

        public async Task<IActionResult> Book(int id)
        {
            var book = await _bookCatalog.GetBook(id);
            var openLibraryBook = await _openLibrary.GetBookByISBN(book.ISBN);
            ViewBag.OpenLibraryBook = openLibraryBook;
            return View(book);
        }

        [HttpPost]
        public async Task<IActionResult> BookPurchase(int id)
        {
            var book = await _bookCatalog.GetBook(id);
            var econtResponse = await _econtService.CreateLabel();
            return View("BookPurchased", econtResponse);
        }

    }
}
