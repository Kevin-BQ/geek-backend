using Models.DTOs;
using Models.Entities;

namespace BLL.Services.Interfaces
{
    public interface IShippingAddressService
    {
        Task<IEnumerable<ShippingAddressDto>> GetAllShippingAddresses();
        Task<ShippingAddressDto> AddShippingAddress(ShippingAddressDto shippingAddressDto);
        Task UpdateShippingAddress(ShippingAddressDto shippingAddressDto);
        Task UpdateStatus(int id);
        Task<IEnumerable<ShippingAddressDto>> GetShippingAddressActive();
    }
}
