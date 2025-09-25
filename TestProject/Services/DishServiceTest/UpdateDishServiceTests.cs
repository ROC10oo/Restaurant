using Application.Exceptions;
using Application.Interfaces.ICategory;
using Application.Interfaces.IDish;
using Application.Models.Request;
using Application.Services.ServicesDish;
using Application.Validator;
using Azure.Core;
using Domain.Entities;
using Domain.Exceptions;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestProject.Services.DishServiceTest
{
    public class UpdateDishServiceTests
    {
        private readonly Mock<IDishCommand> _mockDishCommand = new();
        private readonly Mock<IDishQuery> _mockDishQuery = new();
        private readonly Mock<ICategoryQuery> _mockCategoryQuery = new();
        private readonly IValidatorUpdateDish _validator = new DishValidatorUpdate();

        private readonly UpdateDishService _updateDishService;

        public UpdateDishServiceTests()
        {
            _updateDishService = new UpdateDishService(
                _mockDishCommand.Object,
                _mockDishQuery.Object,
                _mockCategoryQuery.Object,
                _validator
            );
        }

        [Fact]//Modificar el Dish de forma correcta
        public async Task UpdateDish_ShouldUpdateDishSuccessfully() 
        {
            
            var dishId = Guid.Parse("123e4567-e89b-12d3-a456-426614174000");
            var existingDish = new Dish
            {
                DishId = dishId,
                Name = "Pizza Margherita",
                Description = "Original",
                Price = 1000.00m,
                CategoryId = 1,
                Available = true
            };

            var updateRequest = new DishUpdateRequest
            {
                name = "Pizza Margherita Premium",
                description = "Pizza artesanal con mozzarella de búfala, tomates San Marzano, albahaca orgánica y aceite de oliva extra virgen DOP",
                price = 1200.00m,
                category = 1,
                image = "https://restaurant.com/images/pizza-margherita-premium.jpg",
                isActive = true
            };

            //Moqueo que exista el dish que busque
            _mockDishQuery.Setup(q => q.GetDishById(dishId))
                .ReturnsAsync(existingDish);

            //Moqueo la cateogria
            _mockCategoryQuery.Setup(c => c.GetCategoryById(updateRequest.category))
            .ReturnsAsync(new Category
            {
                Id = updateRequest.category,
                Name = "Pizzas"
            });


            _mockDishCommand.Setup(c => c.UpdateDish(It.IsAny<Dish>()))
                .Returns(Task.CompletedTask);


            // Mando el DishRequest al service
            var result = await _updateDishService.UpdateDish(updateRequest, dishId);

            
            Assert.Equal(updateRequest.name, result.name);
            Assert.Equal(updateRequest.price, result.price);
            Assert.Equal(updateRequest.category, result.Category.Id);
            Assert.Equal(updateRequest.description, result.description);
            Assert.Equal(updateRequest.image, result.image);


            _mockDishCommand.Verify(c => c.UpdateDish(It.Is<Dish>(d => d.DishId == dishId && d.Name == updateRequest.name)), Times.Once);
        }



        [Fact]//Modifico un Dish pero no existe
        public async Task UpdateDish_ShouldThrowNotFoundException_WhenDishDoesNotExist()
        {
            var dishId = Guid.NewGuid();
            _mockDishQuery.Setup(q => q.GetDishById(dishId))
                .ReturnsAsync((Dish)null);

            await Assert.ThrowsAsync<DishNotFoundException>(() => _updateDishService.UpdateDish(new DishUpdateRequest(), dishId));
        }



        [Fact]//Modificar pero que no tenga el mismo nombre
        public async Task UpdateDish_ShouldThrowConflictException_WhenNameAlreadyExists()
        {
            var dishId = Guid.NewGuid();
            var existingDish = new Dish { DishId = dishId, Name = "Pizza Margherita" };
            var request = new DishUpdateRequest { name = "Pizza Margherita Premium", price = 1, category = 1};

            _mockDishQuery.Setup(q => q.GetDishById(dishId))
                .ReturnsAsync(existingDish);

            _mockDishQuery.Setup(q => q.GetDishByName(request.name))
                .ReturnsAsync(true);

            await Assert.ThrowsAsync<DishAlreadyExistsException>(() => _updateDishService.UpdateDish(request, dishId));
        }



        [Fact]
        public async Task UpdateDish_ShouldThrowValidationException_WhenPriceIsZeroOrNegative()
        {
            var dishId = Guid.NewGuid();
            var existingDish = new Dish { DishId = dishId, Name = "Pizza" };
            var request = new DishUpdateRequest { price = 0 };

            _mockDishQuery.Setup(q => q.GetDishById(dishId))
                .ReturnsAsync(existingDish);

            await Assert.ThrowsAsync<DishInvalidPriceException>(() => _updateDishService.UpdateDish(request, dishId));
        }


        [Fact]//Modificar un plato con categoria erronea
        public async Task UpdateDish_ShouldThrowCategoryNotFoundException_WhenCategoryDoesNotExist()
        {
            var dishId = Guid.NewGuid();
            var existingDish = new Dish { DishId = dishId, Name = "Pizza" };
            var request = new DishUpdateRequest { price = 1 , category = 999 };


            _mockDishQuery.Setup(q => q.GetDishById(dishId))
                .ReturnsAsync(existingDish);

            //Moqueo la categoria 999 para que no exista
            _mockCategoryQuery.Setup(c => c.GetCategoryById(request.category)).ReturnsAsync((Category)null);


            //Deberia devolver una exception de categoria no encontrada
            await Assert.ThrowsAsync<CategoryNotFoundException>(() => _updateDishService.UpdateDish(request, dishId));
        }
    }

}
