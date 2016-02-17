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
    public class AuthorRepository : iLibrary<Author>
    {
        private LibraryEntities _db;

        public AuthorRepository()
        {
            _db = new LibraryEntities();
        }

        public async Task<Author> delete(int id)
        {
            var author = await _db.Authors.FindAsync(id);
            
            _db.Authors.Remove(author);
            await _db.SaveChangesAsync();

            return author;
        }

        
        public async Task<Author> insert(Author author)
        {            
            _db.Authors.Add(author);
            await _db.SaveChangesAsync();
            return author;
        }

        public async Task<Author> update(int id, Author book)
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

        private bool AuthorExists(int id)
        {
            return _db.Authors.Count(e => e.Id == id) > 0;
        }

        public IQueryable<Author> getAll()
        {
            return _db.Authors;
        }
    }
}