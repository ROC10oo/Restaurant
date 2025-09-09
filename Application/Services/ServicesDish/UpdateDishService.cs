using Application.Interfaces.ICategory;
using Application.Interfaces.IDish;
using Application.Models.Request;
using Application.Models.Response;
using Application.Validator;
using Domain.ErrorsMessages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services.ServicesDish
{
    public class UpdateDishService : IUpdateDishService
    {
        private readonly IDishCommand _dishCommand;
        private readonly IDishQuery _dishQuery;
        private readonly ICategoryQuery _categoryQuery;
        private readonly DishValidatorUpdate _dishValidator;

        public UpdateDishService(IDishCommand dishCommand, IDishQuery dishQuery, ICategoryQuery categoryQuery, DishValidatorUpdate dishValidator)
        {
            _dishCommand = dishCommand;
            _dishQuery = dishQuery;
            _categoryQuery = categoryQuery;
            _dishValidator = dishValidator;
        }

        public async Task<DishResponse> UpdateDish(DishRequestUpdate dish, Guid Id)
        {
            var existsDish = await _dishQuery.GetDishById(Id);
            
            if (existsDish == null)
            {
                throw new Exception(DishErrorMessages.DishNotExists);
            }

            _dishValidator.Validate(dish);


            if (dish.name != existsDish.Name)
            {
                var nameExists = await _dishQuery.GetDishByName(dish.name);
                if (nameExists)
                    throw new Exception(DishErrorMessages.DishAlreadyExists);
            }




            var categoryExists = await _categoryQuery.GetCategoryById(dish.category);
            if (categoryExists == null)
            {
                throw new Exception(DishErrorMessages.CategoryNotExists);
            }


            existsDish.Name = dish.name;
            existsDish.Description = dish.description;
            existsDish.Price = dish.price;
            existsDish.CategoryId = dish.category;
            existsDish.ImageUrl = dish.image;
            existsDish.Available = dish.isActive;
            existsDish.UpdateDate = DateTime.UtcNow;

            await _dishCommand.UpdateDish(existsDish);

            return new DishResponse
            {
                id = existsDish.DishId,
                name = existsDish.Name,
                description = existsDish.Description,
                price = existsDish.Price,
                isActive = existsDish.Available,
                Category = new GenericResponse { Id = categoryExists.Id, Name = categoryExists.Name },
                image = existsDish.ImageUrl,
                createdAt = existsDish.CreateDate,
                updatedAt = existsDish.UpdateDate,
            };

        }
    }
    
}
