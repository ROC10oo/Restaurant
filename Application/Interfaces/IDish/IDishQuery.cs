using Application.Models.Response;
using Domain.Entities;
using Application.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces.IDish
{
    public interface IDishQuery
    {

        Task<bool> GetDishByName(string name);

        Task<Dish> GetDishById(Guid id);

        Task<List<Dish>> GetDishes(string? name = null, int? category = null, OrderByPrice? sortByPrice = OrderByPrice.asc, ActiveFilter onlyActive = ActiveFilter.False);
    }
}
