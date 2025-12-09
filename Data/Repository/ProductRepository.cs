using Data.Interfaces.IRepository;
using Models.DTOs;
using Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Repository
{
    public class ProductRepository : Repository<Product>, IProductRepository
    {
        private readonly ApplicationDbContext _context;

        public ProductRepository(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }

        public void Update(Product product)
        {
            var productDb = _context.Products.FirstOrDefault(e => e.Id == product.Id);

            if (product != null)
            {
                productDb.NameProduct = product.NameProduct;
                productDb.Description = product.Description;
                productDb.LargeDescription = product.LargeDescription;
                productDb.Price = product.Price;
                productDb.Stock = product.Stock;
                productDb.Status = product.Status;
                productDb.Discount = product.Discount;
                productDb.BrandId = product.BrandId;
                productDb.CategoryId = product.CategoryId;
                productDb.SubCategoryId = product.SubCategoryId;

                _context.SaveChanges();
            }
        }
    }
}
