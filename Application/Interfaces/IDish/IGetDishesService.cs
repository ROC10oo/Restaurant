using Application.Enums;
using Application.Models.Response;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces.IDish
{
    public interface IGetDishesService
    {
        Task<IEnumerable<DishResponse>> GetDishes(string name = null, int? category = null, OrderByPrice? sortByPrice = OrderByPrice.asc, ActiveFilter onlyActive = ActiveFilter.True);
    }
}
