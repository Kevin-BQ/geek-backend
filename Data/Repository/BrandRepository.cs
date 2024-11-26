using Data.Interfaces.IRepository;
using Data.Repositorio;
using Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Repository
{
    public class BrandRepository: Repository<Brand>, IBrandRepository
    {
        private readonly ApplicationDbContext _context;

        public BrandRepository(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }

        public void Update(Brand brand)
        {
            var brandDb = _context.Brands.FirstOrDefault(e => e.Id == brand.Id);

            if (brand != null)
            {
                brandDb.NameBrand = brand.NameBrand;
                brandDb.ImageUrl = brand.ImageUrl;
                brandDb.Estado = brand.Estado;

                _context.SaveChanges();
            }
        }
    }
}
