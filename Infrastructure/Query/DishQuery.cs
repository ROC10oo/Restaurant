using Application.Interfaces.IDish;
using Domain.Entities;
using Application.Enums;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Query
{
    public class DishQuery : IDishQuery
    {
        private readonly RestauranteDbContext _context;

        public DishQuery(RestauranteDbContext context)
        {
            _context = context;
        }


        public async Task<Dish> GetDishById(Guid id)
        {
            return await _context.Dishes.FirstOrDefaultAsync(c => c.DishId == id);
        }

        public async Task<bool> GetDishByName(string name)
        {
            return await _context.Dishes.AnyAsync(d => d.Name == name);  
        }




        
        public async Task<List<Dish>> GetDishes(string? name = null, int? category = null, OrderByPrice? sortByPrice = OrderByPrice.asc, ActiveFilter onlyActive = ActiveFilter.True) //Devuelvo una lista y le paso parametros opcionales
        {
            var query = _context.Dishes.AsNoTracking().AsQueryable();

            if (!string.IsNullOrWhiteSpace(name))  
                query = query.Where(d => d.Name.Contains(name));

            if (category.HasValue) 
                query = query.Where(d => d.CategoryId == category.Value);

            query = sortByPrice switch
            {
                OrderByPrice.asc => query.OrderBy(d => d.Price),
                OrderByPrice.desc => query.OrderByDescending(d => d.Price),
                _ => query
            };

            if (onlyActive == ActiveFilter.True)
                query = query.Where(d => d.Available);




            return await query.ToListAsync();
        }

        
    }
}
