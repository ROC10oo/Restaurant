using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces.IOrder
{
    public interface IOrderQuery
    {
        Task<Order> GetOrderById(long orderId);
        Task<IEnumerable<Order>> GetOrders(DateTime? from, DateTime? to, int? statusId);
    }
}
