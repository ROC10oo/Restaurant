using Application.Interfaces.IOrderItem;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Query
{
    public class OrderItemQuery: IOrderItemQuery
    {
        private readonly RestauranteDbContext _context;

        public OrderItemQuery(RestauranteDbContext context)
        {
            _context = context;
        }
        public async Task<bool> ExistsByDishId(Guid dishId)
        {
            return await _context.OrderItems.AnyAsync(o => o.DishId == dishId);
        }
    }
}
