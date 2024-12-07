using AutoMapper;
using BLL.Services.Interfaces;
using Data.Interfaces.IRepositorio;
using Data.Interfaces.IRepository;
using Data.Repositorio;
using Models.DTOs;
using Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Services
{
    public class ShoppingCartService : IShoppingCartService
    {
        private readonly IWorkUnit _workUnit;
        private readonly IMapper _mapper;

        public ShoppingCartService(IWorkUnit workUnit, IMapper mapper)
        {
            _workUnit = workUnit;
            _mapper = mapper;
        }

        public async Task<ShoppingCartDto> AddShoppingCart(ShoppingCartDto shoppingCartDto)
        {
            try
            {

                ShoppingCart shoppingCart = new ShoppingCart
                {
                    UserId = shoppingCartDto.UserId,
                    Discount = shoppingCartDto.Discount,
                };

                await _workUnit.ShoppingCart.Add(shoppingCart);
                await _workUnit.Save();

                if (shoppingCart.Id == 0)
                {
                    throw new TaskCanceledException("No se pudo crear el Carrito");
                }

                return _mapper.Map<ShoppingCartDto>(shoppingCart);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task Remove(int id)
        {
            try
            {
                var shopingCartDb = await _workUnit.ShoppingCart.GetFirst(e => e.Id == id);
                if (shopingCartDb == null)
                {
                    throw new TaskCanceledException("El carrito no Existe");
                }
                _workUnit.ShoppingCart.Remove(shopingCartDb);
                await _workUnit.Save();
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<IEnumerable<ShoppingCartDto>> GetAllShoppingCarts()
        {
            try
            {
                var lista = await _workUnit.ShoppingCart.GetAll(
                                    incluirPropiedades: "User",
                                    orderBy: e => e.OrderBy(e => e.Id));

                return _mapper.Map<IEnumerable<ShoppingCartDto>>(lista.ToList());
            }
            catch (Exception)
            {

                throw;
            }
        }

    }
}
