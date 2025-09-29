using Application.Interfaces.IDish;
using Domain.Entities;
using Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Command
{
    public class DishCommand : IDishCommand
    {
        private readonly RestauranteDbContext _context;

        public DishCommand(RestauranteDbContext context)
        {
            _context = context;
        }

        public async Task CreateDish(Dish dish)
        {
            _context.Add(dish); 
            await _context.SaveChangesAsync();
        }

        public async Task DeleteDish(Dish dish)
        {
            _context.Remove(dish);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateDish(Dish dish)
        {
            _context.Update(dish);
            await _context.SaveChangesAsync();
        }
    }
}
