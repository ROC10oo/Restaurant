using Application.Interfaces.IOrder;
using Application.Interfaces.IOrderItem;
using Application.Models.Request;
using Application.Models.Response;
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
        private readonly IOrderItemCommand _orderItemCommand;

        public UpdateOrderItemForStatusService(IOrderQuery orderQuery)
        {
            _orderQuery = orderQuery;
        }

        public async Task<OrderUpdateReponse> UpdateItemStatus(long orderId, int itemId, OrderItemUpdateRequest request)
        {
            var order = await _orderQuery.GetOrderById(orderId);
            if (order == null)
            {
                throw new Exception("DSADASD");
            }

            var item = order.OrderItems.FirstOrDefault(i => i.OrderItemId == itemId);
            if (item == null)
            {
                throw new Exception("Item not found in the order");
            }

            item.StatusId = request.status;


            await _orderCommand.UpdateOrder(order);

            return new OrderUpdateReponse
            {
                orderNumber = (int)order.OrderId,
                totalAmount = (double)order.Price,
                updateAt = DateTime.UtcNow
            };
        }
    }
}
