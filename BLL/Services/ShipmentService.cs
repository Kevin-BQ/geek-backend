using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using BLL.Services.Interfaces;
using Data.Interfaces.IRepositorio;
using Models.DTOs;
using Models.Entities;
using Models.Enum;
using Stripe.Climate;

namespace BLL.Services
{
    public class ShipmentService : IShipmentService
    {
        private readonly IWorkUnit _workUnit;
        private readonly IMapper _mapper;

        public ShipmentService(IWorkUnit workUnit, IMapper mapper)
        {
            _workUnit = workUnit;
            _mapper = mapper;
        }

        public async Task<ShipmentDto> AddShipment(ShipmentDto shipmentDto)
        {
            try
            {
                Shipment shipment = new Shipment
                {
                    OrderId = shipmentDto.OrderId,
                    ShipmentDate = shipmentDto.ShipmentDate,
                    ShipmentMethod = shipmentDto.ShipmentMethod,
                    StatusShipment = ShipmentStatus.Pendiente,
                };


                await _workUnit.Shipment.Add(shipment);
                await _workUnit.Save();

                if (shipment.Id == 0)
                {
                    throw new TaskCanceledException("No se puedo agrear un envio");
                }

                return _mapper.Map<ShipmentDto>(shipment);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<IEnumerable<ShipmentDto>> GetAllShipments()
        {
            try
            {
                var shipments = await _workUnit.Shipment.GetAll();
                return _mapper.Map<IEnumerable<ShipmentDto>>(shipments);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task UpdateShipment(ShipmentDto shipmentDto)
        {
            try
            {
                var shipmentDb = await _workUnit.Shipment.GetFirst(e => e.Id == shipmentDto.Id);

                shipmentDb.ShipmentMethod = shipmentDto.ShipmentMethod;
                shipmentDb.ShipmentDate = shipmentDto.ShipmentDate;

                _workUnit.Shipment.update(shipmentDb);
                await _workUnit.Save();

            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task UpdateStatus(ShipmentDto shipmentDto)
        {
            try
            {
                var shipmentDb = await _workUnit.Shipment.GetFirst(e => e.Id == shipmentDto.Id);

                if (shipmentDb == null)
                {
                    throw new TaskCanceledException("El envio no Existe");
                }


                if (Enum.TryParse<ShipmentStatus>(shipmentDto.StatusShipment, true, out var status))
                {
                    shipmentDb.StatusShipment = status;
                }

                _workUnit.Shipment.update(shipmentDb);
                await _workUnit.Save();

            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
