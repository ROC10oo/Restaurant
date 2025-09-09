using Application.Models.Request;
using Application.Models.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces.IDish
{
    public interface IUpdateDishService
    {
        Task<DishResponse> UpdateDish(DishRequestUpdate dish, Guid id);
    }
}
