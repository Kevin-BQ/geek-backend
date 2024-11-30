using Data.Interfaces.IRepositorio;
using Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Interfaces.IRepository
{
    public interface ISubcategoryRepository : IGenericRepository<Subcategory>
    {
        void Update(Subcategory subcategory);
    }
}
