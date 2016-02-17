using Quality.Test.bd;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Quality.Test.repositories
{
    interface iLibrary<T>
    {
        IQueryable<T> getAll();

        Task<T> insert(T book);

        Task<T> update(int id, T book);

        Task<T> delete(int id);
    }
}
