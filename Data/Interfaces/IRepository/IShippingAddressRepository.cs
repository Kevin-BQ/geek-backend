using Data.Interfaces.IRepositorio;
using Models.Entities;

namespace Data.Interfaces.IRepository
{
    public interface IShippingAddressRepository: IGenericRepository<ShippingAddress>
    {
        void update(ShippingAddress shippingAddress);
    }
}
