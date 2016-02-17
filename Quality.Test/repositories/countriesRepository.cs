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
    public class countriesRepository : iLibrary<Country>
    {
        private LibraryEntities _db;

        public countriesRepository()
        {
            _db = new LibraryEntities();
        }

        public async Task<Country> delete(int id)
        {
            var country = await _db.Countries.FindAsync(id);
            
            _db.Countries.Remove(country);
            await _db.SaveChangesAsync();

            return country;
        }

        public IQueryable<Country> getAll()
        {
            return _db.Countries;
        }

        public async Task<Country> insert(Country country)
        {            
            _db.Countries.Add(country);
            await _db.SaveChangesAsync();
            return country;
        }

        public async Task<Country> update(int id, Country country)
        {
            _db.Entry(country).State = EntityState.Modified;

            try
            {
                await _db.SaveChangesAsync();
                return country;
            }
            catch (DbUpdateConcurrencyException)
            {
                throw;
            }
        }

        private bool CountryExists(int id)
        {
            return _db.Countries.Count(e => e.Id == id) > 0;
        }
    }
}