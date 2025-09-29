using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces.IOrderItem
{
    public interface IOrderItemCommand
    {
        Task CreateOrderItem(OrderItem item);
        Task InsertOrderItem(List<OrderItem> orderItems);
        Task DeleteOrderItem(IEnumerable<OrderItem> orderItems);
        Task UpdateItem(OrderItem orderItem);
    }
}
