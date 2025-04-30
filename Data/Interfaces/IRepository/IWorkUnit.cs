using Data.Interfaces.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Interfaces.IRepositorio
{
    public interface IWorkUnit: IDisposable
    {
        // Interfaces de los modelos
        IBrandRepository Brand { get; }

        ICategoryRepository Category { get; }

        ISubcategoryRepository Subcategory { get; }

        IProductRepository Product { get; }

        IImageRepository Image { get; }

        IShoppingCartRepository ShoppingCart { get; }

        IUserRepository User { get; }

        IShoppingCarItemsRepository ShoppingCarItem { get; }

        IOrderRepository Order { get; }

        IOrderItemRepository OrderItem { get; }

        IWishlistRepository Wishlist { get; }

        IReviewsRepository Review { get; }

        ICommentRepository Comment { get; }


        Task Save();
    }
}
