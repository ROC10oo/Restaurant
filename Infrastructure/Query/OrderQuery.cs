using Application.Interfaces.IOrder;
using Domain.Entities;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Query
{
    public class OrderQuery : IOrderQuery
    {
        private readonly RestauranteDbContext _context;

        public OrderQuery(RestauranteDbContext context)
        {
            _context = context;
        }

        public async Task<Order> GetOrderById(long orderId)
        {
            return await _context.Orders
                        .Include(o => o.OverallStatus)
                        .Include(o => o.DeliveryType)
                        .Include(o => o.OrderItems)
                            .ThenInclude(oi => oi.Dish)
                        .Include(o => o.OrderItems)
                            .ThenInclude(oi => oi.Status)
                        .FirstOrDefaultAsync(o => o.OrderId == orderId);
        }

        public async Task<IEnumerable<Order>> GetOrders(DateTime? from, DateTime? to, int? statusId)
        {
            var query = _context.Orders
                .Include(o => o.OverallStatus)
                .Include(o => o.DeliveryType)
                .Include(o => o.OrderItems)
                .ThenInclude(oi => oi.Dish)
                .Include(o => o.OrderItems)
                .ThenInclude(oi => oi.Status)
            .AsNoTracking().AsQueryable();

            if (from.HasValue)
            {
                query = query.Where(o => o.CreateDate >= from.Value);
            }
            if (to.HasValue)
            {
                query = query.Where(o => o.CreateDate <= to.Value);
            }
            if (statusId.HasValue)
            {
                query = query.Where(o => o.StatusId == statusId.Value);
            }

            return await query.ToListAsync();
        }
    }
}
