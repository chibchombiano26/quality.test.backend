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
using Quality.Test.repositories;

namespace Quality.Test.webApi
{
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class LibraryController : ApiController
    {

        public LibraryController()
        {
            repository = new Quality.Test.repositories.bookRepository();
        }

        private LibraryEntities db = new LibraryEntities();
        private bookRepository repository;

        // GET: api/Library
        public IQueryable<Book> GetBooks()
        {
            return repository.getAll();
        }

      
        // PUT: api/Library/5
        [ResponseType(typeof(void))]
        public async Task<Book> PutBook(int id, Book book)
        {
            return await repository.update(id, book);
        }

        // POST: api/Library
        [HttpPost]
        public async Task<Book> PostBook(Book book)
        {
            return await repository.insert(book);
        }

        // DELETE: api/Library/5
        [ResponseType(typeof(Book))]
        public async Task<Book> DeleteBook(int id)
        {
            return await repository.delete(id);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        
    }
}