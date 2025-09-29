﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Models.Request;
using Application.Models.Response;

namespace Application.Interfaces.ICategory
{
    public interface IGetCategoriesService
    {
        Task<List<CategoryResponse>> GetAllCategories();
    }
}
