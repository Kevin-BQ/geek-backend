using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Data.Interfaces.IRepository;
using Models.Entities;

namespace Data.Repository
{
    public class ShippingAddressRepository: Repository<ShippingAddress>, IShippingAddressRepository
    {
        private readonly ApplicationDbContext _context;

        public ShippingAddressRepository(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }
        public void update(ShippingAddress shippingAddress)
        {
            var shippingAddressDb = _context.ShippingAddresses.FirstOrDefault(e => e.Id == shippingAddress.Id);
            if (shippingAddress != null)
            {
                shippingAddressDb.Address = shippingAddress.Address;
                shippingAddressDb.City = shippingAddress.City;
                shippingAddressDb.State = shippingAddress.State;
                shippingAddressDb.ZipCode = shippingAddress.ZipCode;
                _context.SaveChanges();
            }
        }
    }
}
