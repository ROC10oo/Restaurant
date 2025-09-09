using Application.Interfaces.ICategory;
using Application.Interfaces.IDish;
using Application.Models.Request;
using Application.Models.Response;
using Azure.Core;
using Domain.Entities;
using Domain.ErrorsMessages;
using Application.Validator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services.ServicesDish
{
    public class CreateDishService : ICreateDishService
    {
        private readonly IDishCommand _dishCommand;
        private readonly IDishQuery _dishQuery;
        private readonly ICategoryQuery _categoryQuery;
        private readonly DishValidatorCreated _dishValidator;

        public CreateDishService(IDishCommand dishCommand, IDishQuery dishQuery, ICategoryQuery categoryQuery, DishValidatorCreated dishValidator)
        {
            _dishCommand = dishCommand;
            _dishQuery = dishQuery;
            _categoryQuery = categoryQuery;
            _dishValidator = dishValidator;
            
        }

        public async Task<DishResponse> CreateDish(DishRequest dish)
        {
            _dishValidator.Validate(dish);


            var ExistsDish = await _dishQuery.GetDishByName(dish.name);
            if (ExistsDish == true)
            {
                throw new Exception(DishErrorMessages.DishAlreadyExists);
            }

            var ExistsCategory = await _categoryQuery.GetCategoryById(dish.category);
            if (ExistsCategory == null)
            {
                throw new Exception(DishErrorMessages.CategoryNotExists);
            }

            var NewDish = new Dish
            {
                Name = dish.name,
                Description = dish.description,
                Price = dish.price,
                CategoryId = dish.category,
                ImageUrl = dish.image,
                Available = true,


            };

            await _dishCommand.CreateDish(NewDish);

            return new DishResponse
            {
                id = NewDish.DishId,
                name = NewDish.Name,
                description = NewDish.Description,
                price = NewDish.Price,
                Category = new GenericResponse { Id = ExistsCategory.Id, Name = ExistsCategory.Name },
                isActive = NewDish.Available,
                image = NewDish.ImageUrl,
                createdAt = NewDish.CreateDate,
                updatedAt = NewDish.UpdateDate,
            };

        }

    }
}

