using Application.Interfaces.IStatus;
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
    public class StatusQuery : IStatusQuery
    {
        private readonly RestauranteDbContext _context;

        public StatusQuery(RestauranteDbContext context)
        {
            _context = context;
        }

        public async Task<List<Status>> GetAllStatus()
        {
            var query = _context.Statuses.AsNoTracking().AsQueryable();
            return await query.ToListAsync();
        }
    }
}
