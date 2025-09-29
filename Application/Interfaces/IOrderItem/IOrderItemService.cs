using Application.Models.Request;
using Application.Models.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces.IOrderItem
{
    public interface IOrderItemService
    {
        Task<OrderItemResponse> CreateOrderItem(OrderItemRequest item);
        Task<OrderItemResponse> InsertOrderItem(List<OrderItemRequest> orderItems);
        Task<OrderItemResponse> DeleteOrderItem(IEnumerable<OrderItemRequest> orderItems);
        Task<OrderItemResponse> UpdateItem(OrderItemRequest orderItem);
    }
}
