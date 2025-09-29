using Application.Exceptions;
using Application.Interfaces.IOrder;
using Application.Models.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services.ServiceOrder
{
    public class GetOrderService : IGetOrderService
    {

        private readonly IOrderQuery _orderQuery;

        public GetOrderService(IOrderQuery orderQuery)
        {
            _orderQuery = orderQuery;
        }

        public async Task<OrderDetailsResponse> GetOrderById(long orderId)
        {
            var order = await _orderQuery.GetOrderById(orderId);

            if (order == null)
            {
                throw new OrderNotFoundException();
            }

            var orderDetails = new OrderDetailsResponse
            {
                orderNumber = (int)order.OrderId,
                totalAmount = (double)order.Price,
                deliveryTo = order.DeliveryTo,
                notes = order.Notes,
                status = new GenericResponse { Id = order.StatusId, Name = order.OverallStatus?.Name ?? "Desconocido" },
                deliveryType = new GenericResponse { Id = order.DeliveryTypeId, Name = order.DeliveryType?.Name ?? "Desconocido" },
                items = order.OrderItems.Select(item => new OrderItemResponse
                {
                    id = 2,
                    quantity = item.Quantity,
                    notes = item.Dish?.Name,
                    dish = new DishShortResponse { id = item.DishId, name = item.Dish?.Name ?? "Desconocido", image = item.Dish?.ImageUrl ?? "No encontrada" },
                    status = new GenericResponse { Id = item.Status.Id, Name = item.Status?.Name ?? "Desconocido" }
                }).ToList(),
                createdAt = order.CreateDate,
                updatedAt = order.UpdateDate
            };
            return orderDetails;

        }
    }
}
