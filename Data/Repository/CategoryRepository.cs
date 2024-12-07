using Data.Interfaces.IRepository;
using Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Repository
{
    public class CategoryRepository : Repository<Category>, ICategoryRepository
    {
        private readonly ApplicationDbContext _context;

        public CategoryRepository(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }

        public void Update(Category category)
        {
            var categoryDb = _context.Categories.FirstOrDefault(e => e.Id == category.Id);

            if (category != null)
            {
                categoryDb.NameCategory = category.NameCategory;
                categoryDb.Status = categoryDb.Status;

                _context.SaveChanges();
            }
        }
    }
}
