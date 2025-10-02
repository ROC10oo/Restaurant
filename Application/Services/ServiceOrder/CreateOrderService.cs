using Application.Exceptions;
using Application.Interfaces.IDeliveryType;
using Application.Interfaces.IDish;
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
    public class CreateOrderService : ICreateOrderService
    {
        private readonly IDishQuery _dishQuery;
        private readonly IDeliveryTypeQuery _deliveryTypeQuery;
        private readonly IOrderCommand _orderCommand;
        private readonly IOrderItemCommand _orderItemCommand;

        public CreateOrderService(IDishQuery dishQuery, IDeliveryTypeQuery deliveryTypeQuery, IOrderCommand orderCommand, IOrderItemCommand orderItemCommand)
        {
            _dishQuery = dishQuery;
            _deliveryTypeQuery = deliveryTypeQuery;
            _orderCommand = orderCommand;
            _orderItemCommand = orderItemCommand;
        }

        public async Task<OrderCreateResponse> CreateOrder(OrderRequest order)
        {
            var deliveryType = await _deliveryTypeQuery.GetDeliveryTypeById(order.delivery.id);

            if (deliveryType == null) 
            {
                throw new InvalidDeliveryException();
            } 

            decimal total = 0;

            foreach (var item in order.items)
            {
                var dish = await _dishQuery.GetDishById(item.id);

                if (item.quantity <= 0)
                    throw new CantInvalidException();


                if (dish == null || !dish.Available)
                    throw new DishNotAvailableOrNotExistsException();

                total += dish.Price * item.quantity;
            }

            var newOrder = new Order
            {
                DeliveryTypeId = order.delivery.id,
                Price = total,
                StatusId = 1,
                DeliveryTo = order.delivery.to,
                Notes = order.notes,
                UpdateDate = DateTime.Now,
                CreateDate = DateTime.Now
            };

            await _orderCommand.CreateOrder(newOrder);

            var listItems = order.items;
            var listorderItems = listItems.Select(item => new OrderItem
            {
                DishId = item.id,
                Quantity = item.quantity,
                Notes = item.notes,
                StatusId = 1,
                OrderId = newOrder.OrderId,
            }).ToList();

            await _orderItemCommand.InsertOrderItem(listorderItems);


            return new OrderCreateResponse
            {
                orderNumber = (int)newOrder.OrderId,
                totalAmount = (decimal)newOrder.Price,
                createdAt = DateTime.Now,
            };

        }
    }
}
