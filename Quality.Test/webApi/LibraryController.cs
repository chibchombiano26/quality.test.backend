using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using Quality.Test.bd;
using System.Web.Http.Cors;

namespace Quality.Test.webApi
{
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class LibraryController : ApiController
    {
        private LibraryEntities db = new LibraryEntities();

        // GET: api/Library
        public IQueryable<Book> GetBooks()
        {
            return db.Books;
        }

        // GET: api/Library/5
        [ResponseType(typeof(Book))]
        public async Task<IHttpActionResult> GetBook(int id)
        {
            Book book = await db.Books.FindAsync(id);
            if (book == null)
            {
                return NotFound();
            }

            return Ok(book);
        }

        // PUT: api/Library/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutBook(int id, Book book)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != book.Id)
            {
                return BadRequest();
            }

            db.Entry(book).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!BookExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/Library
        [HttpPost]
        public async Task<Book> PostBook(Book book)
        {
            if (string.IsNullOrEmpty(book.UrlImage))
            {
                book.UrlImage = "http://i.ytimg.com/vi/wX_KD2craJ0/mqdefault.jpg";
            }


            db.Books.Add(book);
            await db.SaveChangesAsync();
            return book;
        }

        // DELETE: api/Library/5
        [ResponseType(typeof(Book))]
        public async Task<IHttpActionResult> DeleteBook(int id)
        {
            Book book = await db.Books.FindAsync(id);
            if (book == null)
            {
                return NotFound();
            }

            db.Books.Remove(book);
            await db.SaveChangesAsync();

            return Ok(book);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool BookExists(int id)
        {
            return db.Books.Count(e => e.Id == id) > 0;
        }
    }
}