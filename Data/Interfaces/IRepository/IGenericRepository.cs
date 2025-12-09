using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Data.Interfaces.IRepositorio
{
    public interface IGenericRepository<T> where T : class
    {
        Task<IEnumerable<T>> GetAll(
            Expression<Func<T, bool>> filtro = null,
            Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null,
            string incluirPropiedades = null // Include
            );

        Task<T> GetFirst(
            Expression<Func<T, bool>> filtro = null,
            string incluirPropiedades = null // Include
            );

        Task Add(T entidad);
        void Remove(T entidad); 

    }
}
