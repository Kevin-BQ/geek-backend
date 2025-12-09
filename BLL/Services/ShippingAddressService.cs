using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using BLL.Services.Interfaces;
using Data.Interfaces.IRepositorio;
using Microsoft.AspNetCore.Http;
using Models.DTOs;
using Models.Entities;

namespace BLL.Services
{
    public class ShippingAddressService : IShippingAddressService
    {
        private readonly IWorkUnit _workUnit;
        private readonly IMapper _mapper;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public ShippingAddressService(IWorkUnit workUnit, IMapper mapper, IHttpContextAccessor httpContextAccessor)
        {
            _workUnit = workUnit;
            _mapper = mapper;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<ShippingAddressDto> AddShippingAddress(ShippingAddressDto shippingAddressDto)
        {
            try
            {
                var userIdClaim = _httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier);
                if (userIdClaim == null)
                {
                    throw new InvalidOperationException("Usuario no autenticado.");
                }

                int userId = int.Parse(userIdClaim.Value);

                ShippingAddress shippingAddress = new ShippingAddress
                {
                    UserId = userId,
                    Address = shippingAddressDto.Address,
                    City = shippingAddressDto.City,
                    State = shippingAddressDto.State == 1 ? true : false,
                    Country = shippingAddressDto.Country,
                    ZipCode = shippingAddressDto.ZipCode
                };

                await _workUnit.ShippingAddress.Add(shippingAddress);
                await _workUnit.Save();

                if (shippingAddress.Id == 0)
                {
                    throw new TaskCanceledException("No se puedo agrear una nueva Wishlist");
                }

                return _mapper.Map<ShippingAddressDto>(shippingAddress);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<IEnumerable<ShippingAddressDto>> GetAllShippingAddresses()
        {
            try
            {
                var shippingAddresses = await _workUnit.ShippingAddress.GetAll(incluirPropiedades: "User");
                return  _mapper.Map<IEnumerable<ShippingAddressDto>>(shippingAddresses);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<IEnumerable<ShippingAddressDto>> GetShippingAddressActive()
        {
            try
            {
                var userIdClaim = _httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier);
                if (userIdClaim == null)
                {
                    throw new InvalidOperationException("Usuario no autenticado.");
                }

                int userId = int.Parse(userIdClaim.Value);

                var shippingAddresses = await _workUnit.ShippingAddress.GetAll(
                                                                                filtro: e => e.State == true && e.UserId == userId,
                                                                                incluirPropiedades: "User");
                return _mapper.Map<IEnumerable<ShippingAddressDto>>(shippingAddresses);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task UpdateShippingAddress(ShippingAddressDto shippingAddressDto)
        {
            try
            {
                var shiPpingAddressDb = await _workUnit.ShippingAddress.GetFirst(e => e.Id == shippingAddressDto.Id);
                shiPpingAddressDb.Address = shippingAddressDto.Address;
                shiPpingAddressDb.City = shippingAddressDto.City;
                shiPpingAddressDb.Country = shippingAddressDto.Country;
                shiPpingAddressDb.ZipCode = shippingAddressDto.ZipCode;

                _workUnit.ShippingAddress.update(shiPpingAddressDb);
                await _workUnit.Save();

            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task UpdateStatus(int id)
        {
            try
            {
                var shippingAddressDb = await _workUnit.ShippingAddress.GetFirst(e => e.Id == id);
                shippingAddressDb.State = !shippingAddressDb.State;
                _workUnit.ShippingAddress.update(shippingAddressDb);
                await _workUnit.Save();
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
