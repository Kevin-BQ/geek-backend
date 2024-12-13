using AutoMapper;
using BLL.Services.Interfaces;
using Data.Interfaces.IRepositorio;
using Models.DTOs;
using Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Services
{
    public class WishlistService : IWishlistService
    {
        private readonly IWorkUnit _workUnit;
        private readonly IMapper _mapper;

        public WishlistService(IWorkUnit workUnit, IMapper mapper)
        {
            _workUnit = workUnit;
            _mapper = mapper;
        }



        public async Task<IEnumerable<WishlistDto>> GetProducts(int userAplicationId)
        {
            try
            {
                var lista = await _workUnit.Wishlist.GetAll(filtro:e => e.UserAplicationId == userAplicationId,
                                    incluirPropiedades: "Brand,Category,Subcategory,Images",
                                    orderBy: e => e.OrderBy(e => e.Product.NameProduct));

                return _mapper.Map<IEnumerable<WishlistDto>>(lista.ToList());
            }
            catch (Exception)
            {

                throw;
            }
        }


        public async Task<WishlistDto> AddWishlist(WishlistDto wishlistDto)
        {
            try
            {
                Wishlist wishlist = new Wishlist
                {
                    UserAplicationId = wishlistDto.UserId,
                    ProductId = wishlistDto.ProductId,
                };

                await _workUnit.Wishlist.Add(wishlist);
                await _workUnit.Save();

                if (wishlist.Id == 0)
                {
                    throw new TaskCanceledException("No se puedo agrear una nueva Wishlist");
                }

                return _mapper.Map<WishlistDto>(wishlist);
            }
            catch (Exception)
            {

                throw;
            }
        }



        



        public async Task DeleteWishlist(int wishlistId)
        {
            try
            {
                var wishlistDb = await _workUnit.Wishlist.GetFirst(e => e.Id == wishlistId);

                if (wishlistDb == null)
                {
                    throw new TaskCanceledException("La Subcategoría no existe");
                }

                _workUnit.Wishlist.Remove(wishlistDb);

                await _workUnit.Save();
            }
            catch (Exception)
            {

                throw;
            }

        }




    }
}
