using Application.Interfaces.ICategory;
using Application.Models.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services.ServiceCategory
{
    public class GetCategoriesService : IGetCategoriesService
    {


        private readonly ICategoryQuery _categoryQuery;

        public GetCategoriesService(ICategoryQuery categoryQuery)
        {
            _categoryQuery = categoryQuery;
        }


        public async Task<List<CategoryResponse>> GetAllCategories()
        {
            var categories = await _categoryQuery.GetAllCategories();

            return categories.Select(category => new CategoryResponse
            {
                id = category.Id,
                name = category.Name,
                description = category.Description,
                order = category.Order,

            }).ToList();
        }
    }
}
