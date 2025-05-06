using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Data.Interfaces.IRepository;
using Models.DTOs;
using Models.Entities;

namespace Data.Repository
{
    public class ShipmentRepository : Repository<Shipment>, IShipmentRepository
    {

        private readonly ApplicationDbContext _context;

        public ShipmentRepository(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }

        public void update(Shipment shipment)
        {
            var shipmentDb = _context.Shipments.FirstOrDefault(e => e.Id == shipment.Id);

            if (shipment != null)
            {
                shipmentDb.StatusShipment = shipment.StatusShipment;
                shipmentDb.ShipmentDate = shipment.ShipmentDate;
                shipmentDb.ShipmentMethod = shipment.ShipmentMethod;
                _context.SaveChanges();
            }
        }
    }
}
