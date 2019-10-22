using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BookListRazor.Model;
using BookListRazor.Utility;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace BookListRazor.Pages.BookList
{
    public class EditModel : PageModel
    {
        private readonly ApplicationDbContext _db;

        public EditModel(ApplicationDbContext db)
        {
            _db = db;
        }

        [TempData]
        public string Message { get; set; }

        [BindProperty]
        public Book Book { get; set; }

        public async Task OnGet(int id)
        {
            Book = await _db.Book.FindAsync(id).ConfigureAwait(false);
        }

        public async Task<IActionResult> OnPost()
        {
            if (ModelState.IsValid)
            {
                var BookFromDb = await _db.Book.FindAsync(Book.Id).ConfigureAwait(false);
                BookFromDb.Name = Book.Name;
                BookFromDb.Author = Book.Author;
                BookFromDb.ISBN = Book.ISBN;

                await _db.SaveChangesAsync().ConfigureAwait(false);
                Message = SD.EditMessage;
                return RedirectToPage("Index");
            }

            return RedirectToPage();
        }
    }
}