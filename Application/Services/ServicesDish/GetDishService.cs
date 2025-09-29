using Application.Exceptions;
using Application.Interfaces.ICategory;
using Application.Interfaces.IDish;
using Application.Models.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services.ServicesDish
{
    public class GetDishService : IGetDishService
    {
        private readonly IDishQuery _dishQuery;
        private readonly ICategoryQuery _categoryQuery;

        public GetDishService(IDishQuery dishQuery, ICategoryQuery categoryQuery)
        {
            _dishQuery = dishQuery;
            _categoryQuery = categoryQuery;
        }

        public async Task<DishResponse> GetDishById(string id)
        {

            if (!Guid.TryParse(id, out Guid dishId))
            {
                throw new IDInvalidException();

            }

            var dish = await _dishQuery.GetDishById(dishId);

            if (dish == null)
            {
                throw new DishNotFoundException();
            }


            var category = await _categoryQuery.GetCategoryById(dish.CategoryId);

            return new DishResponse
            {
                id = dish.DishId,
                name = dish.Name,
                description = dish.Description,
                price = dish.Price,
                isActive = dish.Available,
                Category = new GenericResponse { Id = category.Id, Name = category.Name },
                image = dish.ImageUrl,
                createdAt = dish.CreateDate,
                updatedAt = dish.UpdateDate,
            };
        }
    }
}
