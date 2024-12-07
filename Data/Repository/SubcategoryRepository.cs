using Data.Interfaces.IRepository;
using Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Repository
{
    internal class SubcategoryRepository : Repository<Subcategory>, ISubcategoryRepository
    {
        private readonly ApplicationDbContext _context;

        public SubcategoryRepository(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }

        public void Update(Subcategory subcategory)
        {
            var subcategoryDb = _context.Subcategories.FirstOrDefault(e => e.Id == subcategory.Id);

            if (subcategory != null)
            {
                subcategoryDb.NameSubcategory = subcategory.NameSubcategory;
                subcategoryDb.Status = subcategoryDb.Status;
                subcategoryDb.CategoryId = subcategory.CategoryId;

                _context.SaveChanges();
            }
        }
    }
}
