using Application.Interfaces.IOrderItem;
using Domain.Entities;
using Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Command
{
    public class OrderItemCommand : IOrderItemCommand
    {
        private readonly RestauranteDbContext _context;

        public OrderItemCommand(RestauranteDbContext context)
        {
            _context = context;
        }



        public async Task CreateOrderItem(OrderItem item)
        {
            _context.Add(item);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteOrderItem(IEnumerable<OrderItem> orderItems)
        {
            _context.OrderItems.RemoveRange(orderItems);
            await _context.SaveChangesAsync();
        }

        public async Task InsertOrderItem(List<OrderItem> orderItems)
        {
            _context.OrderItems.AddRange(orderItems);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateItem(OrderItem orderItem)
        {
            _context.Update(orderItem);
            await _context.SaveChangesAsync();
        }
    }
}
