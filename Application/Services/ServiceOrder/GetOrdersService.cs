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
    public class GetOrdersService : IGetOrdersService
    {
        private readonly IOrderQuery _orderQuery;

        public GetOrdersService(IOrderQuery orderQuery)
        {
            _orderQuery = orderQuery;
        }

        public async Task<IEnumerable<OrderDetailsResponse>> GetOrders(DateTime? from, DateTime? to, int? statusId)
        {


            if (from.HasValue && from.Value < new DateTime(2020, 1, 1))
            {
                throw new InvalidDateRangeException();
            }

            if (to.HasValue && to.Value.Date > DateTime.UtcNow.Date)
            {
                throw new InvalidDateRangeException();
            }

            
            if (from.HasValue && to.HasValue && from.Value.Date > to.Value.Date)
            {
                throw new InvalidDateRangeException();
            }


            var orders = await _orderQuery.GetOrders(from, to, statusId);

            if (orders == null || !orders.Any())
            {
                return Enumerable.Empty<OrderDetailsResponse>();

            }

            var orderResponses = orders.Select(order =>
            new OrderDetailsResponse
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
                    notes = item.Notes,
                    dish = new DishShortResponse { id = item.DishId, name = item.Dish?.Name ?? "Desconocido", image = item.Dish?.ImageUrl ?? "No encontrada" },
                    status = new GenericResponse { Id = item.Status.Id, Name = item.Status?.Name ?? "Desconocido" }
                }).ToList(),
                createdAt = order.CreateDate,
                updatedAt = order.UpdateDate
            });
            return orderResponses;
        }
    }
}
