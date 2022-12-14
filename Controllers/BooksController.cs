using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using LibraryProject.Models.EF;
using Microsoft.Data.SqlClient.Server;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.Security.Claims;

namespace LibraryProject.Controllers
{
    public class BooksController : Controller
    {
        private readonly LibraryAPPDBContext _context = new LibraryAPPDBContext();

        /*public BooksController(LibraryAPPDBContext context)
        {
            _context = context;
        }*/

        string userType = "";
        // GET: Books
        public async Task<IActionResult> Index(string childname)
        {
            ClaimsPrincipal currentUser = this.User;
            try
            {
                userType = currentUser.FindFirst(ClaimTypes.NameIdentifier).Value;
            }
            catch (Exception ex)
            {
                userType = "";
            }

            if (userType == "682b3b24-a126-4a59-a0a6-1c1ab8de9598")
            {
                return _context.Books != null ?
                            View(await _context.Books.ToListAsync()) :
                            Problem("Entity set 'LibraryAPPDBContext.Books'  is null.");

            }
            return _context.Books != null ?
                            View("CustomerIndex", await _context.Books.ToListAsync()) :
                            Problem("Entity set 'LibraryAPPDBContext.Books'  is null.");

        }

        // GET: Books/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Books == null)
            {
                return NotFound();
            }

            var book = await _context.Books
                .FirstOrDefaultAsync(m => m.BookId == id);
            if (book == null)
            {
                return NotFound();
            }

            return View(book);
        }

        // GET: Books/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Books/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("BookId,Isbn,Title,Author,PublishYear,Category")] Book book)
        {
            if (ModelState.IsValid)
            {
                _context.Add(book);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(book);
        }



        // GET: Books/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Books == null)
            {
                return NotFound();
            }

            var book = await _context.Books.FindAsync(id);
            if (book == null)
            {
                return NotFound();
            }
            return View(book);
        }

        // POST: Books/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("BookId,Isbn,Title,Author,PublishYear,Category")] Book book)
        {
            if (id != book.BookId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(book);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!BookExists(book.BookId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(book);
        }

        // GET: Books/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Books == null)
            {
                return NotFound();
            }

            var book = await _context.Books
                .FirstOrDefaultAsync(m => m.BookId == id);
            if (book == null)
            {
                return NotFound();
            }

            return View(book);
        }

        // POST: Books/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Books == null)
            {
                return Problem("Entity set 'LibraryAPPDBContext.Books'  is null.");
            }
            var book = await _context.Books.FindAsync(id);
            if (book != null)
            {
                _context.Books.Remove(book);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool BookExists(int id)
        {
          return (_context.Books?.Any(e => e.BookId == id)).GetValueOrDefault();
        }

        //Rent Now Method
        public static int Transfer(int bookID)
        {
            SqlConnection connection = new SqlConnection("Server= nikhilshah-project-server.database.windows.net; database=libraryAPPDB; user id=Project;password=Cohort@1234");

            SqlCommand cmd3 = new SqlCommand("insert into CustBooks (bookRental) select title from Books where bookID = @bookID", connection);

            cmd3.Parameters.AddWithValue("@bookID", bookID);

            connection.Open();
            cmd3.ExecuteNonQuery();
            connection.Close();

            return (bookID);

        }
        // Rent Page
        public async Task<IActionResult> RentPage(int id)
        {
            if (id == null || _context.Books == null)
            {
                return NotFound();
            }

            var rent = await _context.Books
                .FirstOrDefaultAsync(m => m.BookId == id);
            if (rent == null)
            {
                return NotFound();
            }

            return View(rent);
        }
    }
}



