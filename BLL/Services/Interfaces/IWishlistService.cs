using Models.DTOs;
using Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Services.Interfaces
{
    public interface IWishlistService
    {
        Task<IEnumerable<WishlistDto>> GetMyWishlist();

        Task<WishlistDto> AddWishlist(WishlistDto wishlistDto);

        Task DeleteWishlist(int wishlistId);
    }
}
