using Data.Interfaces.IRepository;
using Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Repository
{
    public class ShoppingCartRepository: Repository<ShoppingCart> , IShoppingCartRepository
    {
        private readonly ApplicationDbContext _context;

        public ShoppingCartRepository(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }

        public void Update(ShoppingCart shoppingCart)
        {
            var shoppingCartDb = _context.ShoppingCarts.FirstOrDefault(e => e.Id == shoppingCart.Id);

            if (shoppingCart != null)
            {
                shoppingCartDb.UserId = shoppingCart.UserId;
                shoppingCartDb.Discount = shoppingCart.Discount;

                _context.SaveChanges();
            }
        }
    }
}
