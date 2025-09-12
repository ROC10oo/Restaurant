using Application.Interfaces.ICategory;
using Application.Interfaces.IDish;
using Application.Models.Response;
using Application.Enums;
using Domain.ErrorsMessages;
using System.Text.RegularExpressions;
using Domain.Exceptions;

namespace Application.Services.ServicesDish
{
    public class GetDishesService:IGetDishesService
    {
        private readonly IDishQuery _dishQuery;
        private readonly ICategoryQuery _categoryQuery;

        

        public GetDishesService(IDishQuery dishQuery, ICategoryQuery categoryQuery)
        {
            _dishQuery = dishQuery;
            _categoryQuery = categoryQuery;
        }


        public async Task<IEnumerable<DishResponse>> GetDishes(string? name = null, int? category = null, OrderByPrice? sortByPrice = OrderByPrice.asc, ActiveFilter onlyActive = ActiveFilter.True)
        {
            
            var dishes = await _dishQuery.GetDishes(name, category, sortByPrice, onlyActive);

            if (!string.IsNullOrWhiteSpace(name))
            {
                if (!Regex.IsMatch(name, @"^[\p{L}0-9\s,\.\-]+$"))
                {
                    throw new InvalidParameterException();
                }
            }


            if (category.HasValue)
            {
                var categoryExists = await _categoryQuery.GetCategoryById(category.Value);
                if (categoryExists == null)
                {
                    throw new InvalidParameterException();

                }
            }

            return dishes.Select(d => new DishResponse
            {
                id = d.DishId,
                name = d.Name,
                description = d.Description,
                price = d.Price,
                isActive = d.Available,
                Category = new GenericResponse
                {
                    Id = d.CategoryId,
                    Name = d.Category?.Name
                },
                image = d.ImageUrl,
                createdAt = d.CreateDate,
                updatedAt = d.UpdateDate
            }).ToList();
        }

    }
}
