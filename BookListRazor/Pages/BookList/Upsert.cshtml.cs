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
    public class UpsertModel : PageModel
    {
        private ApplicationDbContext _db;

        public UpsertModel (ApplicationDbContext db)
        {
            _db = db;
        }

        [BindProperty]
        public Book Book { get; set; }

        // int id has been passed into this when Edit is selected
        public async Task<IActionResult> OnGet(int? id)
        {
            Book = new Book();
            if(id ==null)
            {
                // create
                return Page();
            }
            // update
            Book = await _db.Book.FirstOrDefaultAsync(u => u.Id == id);
            if(Book==null)
            {
                return NotFound();
            }
            return Page();
            
        }

        //post handler

        public async Task<IActionResult> OnPost()
        {
            if (ModelState.IsValid)
            {
              if(Book.Id==0)
                {
                    _db.Book.Add(Book);
                }
                else
                {
                    _db.Book.Update(Book);
                }
                // update the book object in the db
                await _db.SaveChangesAsync();
                return RedirectToPage("Index");
            }
            else
            {
                return RedirectToPage();
            }


        }


    }
}