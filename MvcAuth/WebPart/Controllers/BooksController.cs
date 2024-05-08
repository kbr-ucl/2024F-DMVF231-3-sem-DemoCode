using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebPart.Models;

namespace WebPart.Controllers
{
    public class BooksController : Controller
    {

        private List<BookViewModel> _books = new List<BookViewModel>
        {
            new BookViewModel { Id = 1, Title = "Book 1", Description = "Description 1" },
            new BookViewModel { Id = 2, Title = "Book 2", Description = "Description 2" },
            new BookViewModel { Id = 3, Title = "Book 3", Description = "Description 3" }
        };
        public ActionResult Index()
        {

            return View(_books);
        }

        // GET: BooksController/Details/5
        public ActionResult Details(int id)
        {
            return View(_books.FirstOrDefault(a => a.Id == id));
        }

        // GET: BooksController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: BooksController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: BooksController/Edit/5
        public ActionResult Edit(int id)
        {
            return View(_books.FirstOrDefault(a => a.Id == id));
        }

        // POST: BooksController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: BooksController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: BooksController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
