using Application.Models.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces.IOrder
{
    public interface IGetOrdersService
    {
        Task<IEnumerable<OrderDetailsResponse>> GetOrders(DateTime? from, DateTime? to, int? statusId);
    }
}
