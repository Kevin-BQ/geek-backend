using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Models.DTOs;
using Models.Entities;

namespace BLL.Services.Interfaces
{
    public interface IShipmentService
    {
        Task<IEnumerable<ShipmentDto>> GetAllShipments();
        Task<ShipmentDto> AddShipment(ShipmentDto shipmentDto);
        Task UpdateShipment(ShipmentDto shipmentDto);
        Task UpdateStatus(ShipmentDto shipmentDto);
    }
}
