using Application.Interfaces.ICategory;
using Domain.Entities;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Command
{
    public class CategoryCommand : ICategoryCommand
    {
        private readonly RestauranteDbContext _context;

        public CategoryCommand(RestauranteDbContext context)
        {
            _context = context;
        }

        public async Task CreateCategory(Category category)
        {
            _context.Add(category);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateCategory(Category category)
        {
            _context.Update(category);
            await _context.SaveChangesAsync();
        }
    }
}
