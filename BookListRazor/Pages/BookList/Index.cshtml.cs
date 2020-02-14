using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BookListRazor.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace BookListRazor
{
    public class IndexModel : PageModel
    {
        // create an object of applicationDbContext to use the db

        private readonly ApplicationDbContext _db;

        // constructor
        // this gets applicationdbcontext
        public IndexModel(ApplicationDbContext db)
        {
            _db = db;
        }

        public IEnumerable<Book> Books { get; set; }


        public async Task OnGet()
        {
            // extract a list of books from db
            Books = await _db.Book.ToListAsync();
        }

        public async Task<IActionResult> OnPostDelete(int id)
        {
            var book = await _db.Book.FindAsync(id);
            if(book == null)
            {
                return NotFound();
            }

            // if we find the book
            _db.Book.Remove(book);
            await _db.SaveChangesAsync();
            // return back to the page
            return RedirectToPage("Index");
        }
    }
}