using Application.Exceptions;
using Application.Interfaces.IOrder;
using Application.Interfaces.IOrderItem;
using Application.Models.Request;
using Application.Models.Response;
using Domain.Entities;
using Domain.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services.ServiceOrder
{
    public class UpdateOrderItemForStatusService : IUpdateOrderItemForStatusService
    {
        private readonly IOrderQuery _orderQuery;
        private readonly IOrderCommand _orderCommand;

        public UpdateOrderItemForStatusService(IOrderQuery orderQuery, IOrderCommand orderCommand)
        {
            _orderQuery = orderQuery;
            _orderCommand = orderCommand; 
        }

        public async Task<OrderUpdateReponse> UpdateItemStatus(long OrderId, int itemId, OrderItemUpdateRequest request)
        {
            var order = await _orderQuery.GetOrderById(OrderId);
            Console.WriteLine($"Buscando orden {OrderId}, encontrada: {order != null}");
            if (order == null)
            {
                throw new OrderNotFoundException();
            }

            //Buscar el item dentro de la orden
            var item = order.OrderItems.FirstOrDefault(i => i.OrderItemId == itemId);
            if (item == null)
            {
                throw new OrderItemNotFoundException();
            }


            //valido el estado que me mandan
            var validStatuses = new[] { 1, 2, 3, 4, 5 };
            if (!validStatuses.Contains(request.status))
            {
                throw new ValidationStatusException();
            }

            if (item.StatusId == 1 && request.status != 2 && request.status != 5)
            {
                throw new InvalidTransactionException("Pendiente solo puede ir a 'En preparación' o 'Cancelado'.");
            }


            if (item.StatusId == 2 && request.status != 3 && request.status != 5)
            {
                throw new InvalidTransactionException("'En preparación' solo puede ir a 'Listo' o 'Cancelado'.");
            }


            if (item.StatusId == 3 && request.status != 4)
            {
                throw new InvalidTransactionException("'Listo' solo puede ir a 'Entregado'.");
            }


            if (item.StatusId == 4)
            {
                throw new InvalidTransactionException("No se puede cambiar un ítem 'Entregado'.");
            }

            if (item.StatusId == 5)
            { 
                throw new InvalidTransactionException("No se puede cambiar un ítem 'Cancelado'.");
            }

            item.StatusId = request.status;

            //Actualizo la orden general
            UpdateOrderStatus(order);


            await _orderCommand.UpdateOrder(order);

            return new OrderUpdateReponse
            {
                orderNumber = (int)order.OrderId,
                totalAmount = (double)order.Price,
                updateAt = DateTime.UtcNow
            };
        }


        
                



        private void UpdateOrderStatus(Order order)
        {
            var items = order.OrderItems;

            // Todos cancelados → Cancelada
            if (items.All(i => i.StatusId == 5))
            {
                order.StatusId = 5;
                return;
            }

            // Todos entregados → Entregada
            if (items.All(i => i.StatusId == 4))
            {
                order.StatusId = 4;
                return;
            }

            // Todos listos → Listo para entrega
            if (items.All(i => i.StatusId == 3))
            {
                order.StatusId = 3;
                return;
            }

            // Al menos uno en preparación → En preparación
            if (items.Any(i => i.StatusId == 2))
            {
                order.StatusId = 2;
                return;
            }

            // Si ninguno ha comenzado → Pendiente
            if (items.All(i => i.StatusId == 1))
            {
                order.StatusId = 1;
                return;
            }

            order.StatusId = 1; // Pendiente
        }
    }
}
