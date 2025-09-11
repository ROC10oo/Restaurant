using Application.Interfaces.ICategory;
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
    public class CategoryQuery : ICategoryQuery
    {

        private readonly RestauranteDbContext _context;

        public CategoryQuery(RestauranteDbContext context)
        {
            _context = context;
        }

        public Task<List<Category>> GetAllCategories()
        {
            throw new NotImplementedException();
        }

        public async Task<Category> GetCategoryById(int id)
        {
            return await _context.Categories.FindAsync(id).AsTask();
        }
    }
}
