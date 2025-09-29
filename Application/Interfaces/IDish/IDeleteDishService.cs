﻿using Application.Models.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces.IDish
{
    public interface IDeleteDishService
    {
        Task<DishResponse> DeleteDish(Guid id);
    }
}
