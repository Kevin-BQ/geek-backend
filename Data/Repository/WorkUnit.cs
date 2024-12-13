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
        public IProductRepository Product { get; private set; }
        public IImageRepository Image { get; private set; }
        public IShoppingCartRepository ShoppingCart { get; private set; }
        public IUserRepository User { get; private set; }
        public IShoppingCarItemsRepository ShoppingCarItem { get; private set; }
        public IOrderRepository Order { get; private set; }
        public IOrderItemRepository OrderItem { get; private set; }
        public IWishlistRepository Wishlist { get; private set; }

        public WorkUnit(ApplicationDbContext context)
        {
            _context = context;
            // 
            Brand = new BrandRepository(context);
            Category = new CategoryRepository(context);
            Subcategory = new SubcategoryRepository(context);
            Product = new ProductRepository(context);
            Image = new ImageRepository(context);
            ShoppingCart = new ShoppingCartRepository(context);
            User = new UserRepository(context);
            ShoppingCarItem = new ShoppingCartItemsRepository(context);
            Order = new OrderRepository(context);
            OrderItem = new OrderItemRepository(context);
            Wishlist = new WishlistRepository(context);
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
