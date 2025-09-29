using Application.Interfaces.IDeliveryType;
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
    public class DeliveryTypeQuery : IDeliveryTypeQuery
    {
        private readonly RestauranteDbContext _context;

        public DeliveryTypeQuery(RestauranteDbContext context)
        {
            _context = context;
        }

        public async Task<List<DeliveryType>> GetAllDeliverysTypes()
        {
            var query = _context.DeliveryTypes.AsNoTracking().AsQueryable();
            return await query.ToListAsync();
        }

        public async Task<DeliveryType> GetDeliveryTypeById(int id)
        {
            return await _context.DeliveryTypes.FindAsync(id).AsTask();
        }
    }
}
