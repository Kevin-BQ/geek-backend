using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Data.Interfaces.IRepositorio;
using Models.DTOs;
using Models.Entities;

namespace Data.Interfaces.IRepository
{
    public interface IShipmentRepository: IGenericRepository<Shipment>
    {
        void update(Shipment shipment);
    }
}
