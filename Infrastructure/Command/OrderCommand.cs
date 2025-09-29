using Application.Interfaces.IOrder;
using Domain.Entities;
using Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Command
{
    public class OrderCommand : IOrderCommand
    {
        private readonly RestauranteDbContext _context;

        public OrderCommand(RestauranteDbContext context)
        {
            _context = context;
        }

        public async Task CreateOrder(Order order)
        {
            _context.Add(order);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateOrder(Order order)
        {
            _context.Update(order);
            await _context.SaveChangesAsync();
        }
    }
}
