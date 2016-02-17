using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using Quality.Test.bd;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using Quality.Test.repositories;

namespace Quality.Test.repositories
{
    public class bookRepository : iLibrary<Book>
    {
        private LibraryEntities _db;

        public bookRepository()
        {
            _db = new LibraryEntities();
        }

        public async Task<Book> delete(int id)
        {
            Book book = await _db.Books.FindAsync(id);
            
            _db.Books.Remove(book);
            await _db.SaveChangesAsync();

            return book;
        }

        public IQueryable<Book> getAll()
        {
            return _db.Books;
        }

        public async Task<Book> insert(Book book)
        {
            if (string.IsNullOrEmpty(book.UrlImage))
            {
                book.UrlImage = "http://i.ytimg.com/vi/wX_KD2craJ0/mqdefault.jpg";
            }


            _db.Books.Add(book);
            await _db.SaveChangesAsync();
            return book;
        }

        public async Task<Book> update(int id, Book book)
        {
            _db.Entry(book).State = EntityState.Modified;

            try
            {
                await _db.SaveChangesAsync();
                return book;
            }
            catch (DbUpdateConcurrencyException)
            {
                throw;
            }
        }

        private bool BookExists(int id)
        {
            return _db.Books.Count(e => e.Id == id) > 0;
        }
    }
}