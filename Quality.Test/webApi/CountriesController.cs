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
    public class CountriesController : ApiController
    {
        private countriesRepository repository;

        public CountriesController()
        {
            repository = new countriesRepository();
        }


        // GET: api/Countries
        public IQueryable<Country> GetCountries()
        {
            return repository.getAll();
        }
        
        // PUT: api/Countries/5
        [ResponseType(typeof(void))]
        public async Task<Country> PutCountry(short id, Country country)
        {
            return await repository.update(id, country);
        }

        // POST: api/Countries
        [ResponseType(typeof(Country))]
        public async Task<Country> PostCountry(Country country)
        {
            return await repository.insert(country);
        }

        // DELETE: api/Countries/5
        [ResponseType(typeof(Country))]
        public async Task<Country> DeleteCountry(short id)
        {
            return await repository.delete(id);
        }        
    }
}