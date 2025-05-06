using AutoMapper;
using BLL.Services.Interfaces;
using Data;
using Data.Interfaces.IRepositorio;
using Microsoft.AspNetCore.Http;
using Models.DTOs;
using Models.Entities;
using Models.Enum;
using Stripe.Climate;
using System.Security.Claims;


namespace BLL.Services
{
    public class OrderService : IOrderService
    {
        private readonly IWorkUnit _workUnit;
        private readonly IMapper _mapper;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly ApplicationDbContext _context;
        private readonly IStripeService _stripeService;

        public OrderService(IWorkUnit workUnit, IMapper mapper,
            IHttpContextAccessor httpContextAccessor, ApplicationDbContext context, IStripeService stripeService)
        {
            _workUnit = workUnit;
            _mapper = mapper;
            _httpContextAccessor = httpContextAccessor;
            _context = context;
            _stripeService = stripeService;
        }

        public async Task<OrderDto> AddOrder(OrderDto orderDto)
        {
            using var transaction = await _context.Database.BeginTransactionAsync();
            try
            {
                var userIdClaim = _httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier);
                if (userIdClaim == null) throw new InvalidOperationException("Usuario no autenticado.");

                int userId = int.Parse(userIdClaim.Value);

                var shippingAddress = await _workUnit.ShippingAddress.GetFirst(e => e.Id == orderDto.ShippingAddressId && e.UserId == userId) 
                    ?? throw new InvalidOperationException("Dirección de envío no válida");

                var shoppingCart = await _workUnit.ShoppingCart.GetFirst(e => e.UserId == userId);
                if (shoppingCart == null) throw new InvalidOperationException("Carrito no encontrado");

                var listaShopingCartItem = await _workUnit.ShoppingCarItem.GetAll(e => e.CartId == shoppingCart.Id);
                if (!listaShopingCartItem.Any()) throw new InvalidOperationException("Carrito vacío");

                Models.Entities.Order order = new Models.Entities.Order
                {
                    UserId = userId,
                    ShippingAddressId = shippingAddress.Id,
                    OrderDate = DateTime.Now,
                    RequiredDate = DateTime.Now.AddDays(7),
                    Status = orderDto.Status == 1, 
                    OrderStatus = OrderStatus.Pendiente,
                    Total = 0
                };

                await _workUnit.Order.Add(order);
                await _workUnit.Save();

                if (order.Id == 0) throw new InvalidOperationException("Error al crear la orden");

                foreach (var item in listaShopingCartItem)
                {
                    var product = await _workUnit.Product.GetFirst(p => p.Id == item.ProductId)
                        ?? throw new InvalidOperationException($"Producto {item.ProductId} no encontrado");

                    if (product.Stock < item.Quantity)
                        throw new InvalidOperationException($"Stock insuficiente para {product.NameProduct}");

                    decimal itemTotal = item.Price * item.Quantity * (1 - (decimal)item.Product.Discount);
                    order.Total += itemTotal;

                    var OrderItem = new OrderItem
                    {
                        OrderId = order.Id,
                        ProductId = item.ProductId,
                        Quantity = item.Quantity,
                        ListPrice = item.Price,
                        Discount = (decimal)item.Product.Discount
                    };
                    await _workUnit.OrderItem.Add(OrderItem);
                }
                _workUnit.Order.Update(order);
                await _workUnit.Save();

                var sessionId = await _stripeService.CreateStripeSession(order.Id);
                if (string.IsNullOrEmpty(sessionId)) throw new InvalidOperationException("Error al crear sesión de Stripe");

                order.SessionId = sessionId;

                _workUnit.Order.Update(order);
                await _workUnit.Save();

                Shipment shipment = new Shipment
                {
                    OrderId = order.Id,
                    ShipmentDate = orderDto.ShippingDate,
                    ShipmentMethod = orderDto.ShippingMethod,
                    StatusShipment = ShipmentStatus.Pendiente,
                };


                await _workUnit.Shipment.Add(shipment);
                await _workUnit.Save();

                await transaction.CommitAsync();
                return _mapper.Map<OrderDto>(order);
            }
            catch (Exception)
            {
                await transaction.RollbackAsync();
                throw;
            }
        }

        public async Task<string> ConfirmPayment(int orderId)
        {
            using var transaction = await _context.Database.BeginTransactionAsync();
            try
            {
                var order = await _workUnit.Order.GetFirst(o => o.Id == orderId)
                    ?? throw new InvalidOperationException("Orden no encontrada");

                if (order.OrderStatus != OrderStatus.Pendiente)
                    throw new InvalidOperationException("La orden ya fue procesada");

                var session = await _stripeService.GetSession(order.SessionId);

                if (session.PaymentStatus != "paid" || session.Status != "complete")
                    throw new InvalidOperationException("Pago no completado");

                var expectedAmount = (long)(order.Total * 100);
                if (session.AmountTotal != expectedAmount)
                    throw new InvalidOperationException("Monto pagado no coincide");

                var shipment = await _workUnit.Shipment.GetFirst(s => s.OrderId == orderId);
                shipment.StatusShipment = ShipmentStatus.Enviado;
                _workUnit.Shipment.update(shipment);

                var orderItems = await _workUnit.OrderItem.GetAll(oi => oi.OrderId == orderId);
                foreach (var item in orderItems)
                {
                    var product = await _workUnit.Product.GetFirst(p => p.Id == item.ProductId)
                        ?? throw new InvalidOperationException($"Producto {item.ProductId} no encontrado");

                    product.Stock -= item.Quantity;
                    _workUnit.Product.Update(product);
                }

                var shoppingCart = await _workUnit.ShoppingCart.GetFirst(c => c.UserId == order.UserId);
                if (shoppingCart != null)
                {
                    var cartItems = await _workUnit.ShoppingCarItem.GetAll(c => c.CartId == shoppingCart.Id);
                    foreach (var item in cartItems)
                    {
                        _workUnit.ShoppingCarItem.Remove(item);
                    }
                }

                order.OrderStatus = OrderStatus.Pagado;
                _workUnit.Order.Update(order);

                await _workUnit.Save();
                await transaction.CommitAsync();
                return "¡Compra confirmada exitosamente!";
            }
            catch (Exception)
            {
                await transaction.RollbackAsync();
                throw;
            }
        }

        public async Task Remove(int orderId)
        {
            try
            {
                var orderDb = await _workUnit.Order.GetFirst(e => e.Id == orderId);
                if (orderDb == null)
                {
                    throw new TaskCanceledException("La orden no Existe");
                }

                orderDb.Status = !orderDb.Status;
                orderDb.OrderStatus = OrderStatus.Cancelado;

                _workUnit.Order.Update(orderDb);
                await _workUnit.Save();
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<IEnumerable<OrderDto>> GetAllOrders()
        {
            try
            {
                var userIdClaim = _httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier);
                if (userIdClaim == null)
                {
                    throw new InvalidOperationException("Usuario no autenticado.");
                }

                int userId = int.Parse(userIdClaim.Value);

                var lista = await _workUnit.Order.GetAll(
                                    filtro: e => e.UserId == userId,
                                    incluirPropiedades: "User,ShippingAddress,Shipment",
                                    orderBy: e => e.OrderBy(e => e.Id));

                return _mapper.Map<IEnumerable<OrderDto>>(lista.ToList());
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task UpdateStatusOrder(OrderDto orderDto)
        {
            try
            {
                var orderDb = await _workUnit.Order.GetFirst(e => e.Id == orderDto.Id);
                if (orderDb == null)
                {
                    throw new TaskCanceledException("La orden no Existe");
                }

                if (Enum.TryParse<OrderStatus>(orderDto.OrderStatus, true, out var status))
                {
                    orderDb.OrderStatus = status;
                }

                _workUnit.Order.Update(orderDb);
                await _workUnit.Save();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<OrderDto> GetOrder(int orderId)
        {
            try
            {
                var userIdClaim = _httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier);
                if (userIdClaim == null)
                {
                    throw new InvalidOperationException("Usuario no autenticado.");
                }
                int userId = int.Parse(userIdClaim.Value);
                var orderDb = await _workUnit.Order.GetFirst(e => e.Id == orderId && e.UserId == userId,
                    incluirPropiedades: "User,ShippingAddress,Shipment");
                return _mapper.Map<OrderDto>(orderDb);
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
