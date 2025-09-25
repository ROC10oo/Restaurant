using Application.Exceptions;
using Application.Interfaces.ICategory;
using Application.Interfaces.IDish;
using Application.Services.ServicesDish;
using Domain.Entities;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestProject.Services.DishServiceTest
{
    public class GetDishServiceTests
    {
        private readonly Mock<IDishQuery> _mockDishQuery = new();
        private readonly Mock<ICategoryQuery> _mockCategoryQuery = new();
        private readonly GetDishesService _service;

        public GetDishServiceTests()
        {
            _service = new GetDishesService(_mockDishQuery.Object, _mockCategoryQuery.Object);
        }

        [Fact] //Devuelve lista de platos
        public async Task GetDishes_ShouldReturnList_WhenDishesExist()
        {
            // Arrange
            var dishes = new List<Dish>
            {
                new Dish
                {
                    DishId = Guid.NewGuid(),
                    Name = "Pizza Margherita",
                    Description = "Clásica",
                    Price = 850,
                    CategoryId = 1,
                    Category = new Category { Id = 1, Name = "Pizzas" },
                    Available = true,
                    ImageUrl = "url",
                    CreateDate = DateTime.UtcNow,
                    UpdateDate = DateTime.UtcNow
                }
            };

            _mockDishQuery.Setup(q => q.GetDishes("Pizza", null, null, true))
                          .ReturnsAsync(dishes);

            // Act
            var result = await _service.GetDishes("Pizza", null, null, true);

            // Assert
            Assert.NotNull(result);
            Assert.Single(result);
            Assert.Equal("Pizza Margherita", result.First().name);
        }


        [Fact] // Devuelve lista vacía
        public async Task GetDishes_ShouldReturnEmptyList_WhenNoDishesExist()
        {
            
            _mockDishQuery.Setup(q => q.GetDishes(null, null, null, true))
                          .ReturnsAsync(new List<Dish>());

            // Mando en el service todo vacio, sin restrinciones
            var result = await _service.GetDishes(null, null, null, true);

            // Deberia devolver la lista vacia si no hay nada
            Assert.NotNull(result);
            Assert.Empty(result);
        }




        [Fact] // Nombre inválido
        public async Task GetDishes_ShouldThrowInvalidParameterException_WhenNameContainsInvalidCharacters()
        {
            // Ejemplo del nombre invalido
            string invalidName = "Pizza@#";

            //Moqueo el query con el nombre invalido
            _mockDishQuery.Setup(q => q.GetDishes(invalidName, null, null, true))
                          .ReturnsAsync(new List<Dish>());

            // Mando todo al service y me devuelve una exception si el nombre esta mal
            await Assert.ThrowsAsync<InvalidParameterException>(
                () => _service.GetDishes(invalidName, null, null, true)
            );
        }



        [Fact] //Categoría no existe
        public async Task GetDishes_ShouldThrowInvalidParameterException_WhenCategoryDoesNotExist()
        {
            // Categoria inexistente
            int categoryId = 999;
            _mockDishQuery.Setup(q => q.GetDishes(null, categoryId, null, true))
                          .ReturnsAsync(new List<Dish>());

            _mockCategoryQuery.Setup(c => c.GetCategoryById(categoryId))
                              .ReturnsAsync((Category)null);

            
            await Assert.ThrowsAsync<InvalidParameterException>(
                () => _service.GetDishes(null, categoryId, null, true)
            );
        }
    }
}
