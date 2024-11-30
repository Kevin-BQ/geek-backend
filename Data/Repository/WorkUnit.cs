using Data.Interfaces.IRepositorio;
using Data.Interfaces.IRepository;
using Data.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Repositorio
{
    public class WorkUnit : IWorkUnit
    {
        private readonly ApplicationDbContext _context;
        // Interfaces de Repositorios 
        public IBrandRepository Brand {  get; private set; }
        public ICategoryRepository Category { get; private set; }
        public ISubcategoryRepository Subcategory { get; private set; }

        public WorkUnit(ApplicationDbContext context)
        {
            _context = context;
            // 
            Brand = new BrandRepository(context);
            Category = new CategoryRepository(context);
            Subcategory = new SubcategoryRepository(context);
        }

        public void Dispose()
        {
            _context.Dispose();
        }

        public async Task Save()
        {
            await _context.SaveChangesAsync();
        }
    }
}
