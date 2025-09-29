using Application.Exceptions;
using Application.Interfaces.IDish;
using Application.Interfaces.IOrder;
using Application.Interfaces.IOrderItem;
using Application.Models.Request;
using Application.Models.Response;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services.ServiceOrder
{
    public class UpdateOrderService : IUpdateOrderService
    {
        private readonly IOrderCommand _orderCommand;
        private readonly IOrderQuery _orderQuery;
        private readonly IOrderItemCommand _orderItemCommand;
        private readonly IDishQuery _dishQuery;

        public UpdateOrderService(IOrderCommand orderCommand, IOrderQuery orderQuery, IOrderItemCommand orderItemCommand, IDishQuery dishQuery)
        {
            _orderCommand = orderCommand;
            _orderQuery = orderQuery;
            _orderItemCommand = orderItemCommand;
            _dishQuery = dishQuery;
        }

        public async Task<OrderUpdateReponse> UpdateOrder(OrderUpdateRequest orders, long orderId)
        {
            var order = await _orderQuery.GetOrderById(orderId);
            if (order == null)
            {
                throw new OrderNotFoundException();
            }

            if (order.OverallStatus.Id != 1)
            {
                throw new OrderInPreparationException();
            }

            await _orderItemCommand.DeleteOrderItem(order.OrderItems);

            var newOrderItems = orders.items.Select(item => new OrderItem
            {
                OrderId = orderId,
                DishId = item.id,
                Quantity = item.quantity,
                Notes = item.notes,
                StatusId = 1
            }).ToList();


            decimal totalPrice = 0;

            foreach (var item in newOrderItems)
            {
                var dish = await _dishQuery.GetDishById(item.DishId);
                if (dish != null)
                {
                    totalPrice += dish.Price * item.Quantity;
                }

                if (!dish.Available)
                {
                    throw new DishNotAvailableException();
                }
            }

            await _orderItemCommand.InsertOrderItem(newOrderItems);

            order.Price = totalPrice;
            order.UpdateDate = DateTime.Now;

            await _orderCommand.UpdateOrder(order);

            return new OrderUpdateReponse
            {
                orderNumber = (int)order.OrderId,
                totalAmount = (double)order.Price,
                updateAt = order.UpdateDate
            };
        }
    }
}
