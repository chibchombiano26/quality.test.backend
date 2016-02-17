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
    public class AuthorsController : ApiController
    {
        private AuthorRepository repository;

        public AuthorsController()
        {
            repository = new Quality.Test.repositories.AuthorRepository();
        }
        

        // GET: api/Authors
        public IQueryable<Author> GetAuthors()
        {
            return repository.getAll();
        }

        // PUT: api/Authors/5
        [ResponseType(typeof(void))]
        public async Task<Author> PutAuthor(short id, Author author)
        {
            return await repository.update(id, author);
        }

        // POST: api/Authors
        [ResponseType(typeof(Author))]
        public async Task<Author> PostAuthor(Author author)
        {
            return await repository.insert(author);
        }

        // DELETE: api/Authors/5
        [ResponseType(typeof(Author))]
        public async Task<Author> DeleteAuthor(short id)
        {
            return await repository.delete(id);
        }        
    }
}