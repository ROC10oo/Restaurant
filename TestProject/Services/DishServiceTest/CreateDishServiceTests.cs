using Application.Interfaces.ICategory;
using Application.Interfaces.IDish;
using Application.Models.Request;
using Application.Services.ServicesDish;
using Application.Validator;
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
    public class CreateDishServiceTests
    {
        private readonly Mock<IDishCommand> _mockDishCommand = new();
        private readonly Mock<IDishQuery> _mockDishQuery = new();
        private readonly Mock<ICategoryQuery> _mockCategoryQuery = new();
        private readonly IValidatorCreatedDish _validator = new DishValidatorCreated();
        private readonly CreateDishService _createDishService;


        public CreateDishServiceTests()
        {
            _createDishService = new CreateDishService(
                _mockDishCommand.Object,
                _mockDishQuery.Object,
                _mockCategoryQuery.Object,
                _validator
            );
        }

        [Fact] //Creacion de un plato valido
        public async Task CreateDish_ShouldReturnDishResponse_WhenValid()
        {
            //Creo el DishRequest con todos los datos validos
            var request = new DishRequest            
            {
                name = "Pizza Margherita",
                description = "Deliciosa",
                price = 850.50M,
                category = 1,
                image = "https://restaurant.com/images/pizza-margherita.jpg"
            };

            //Moqueo la categoria para que exista la categoria 1
            _mockCategoryQuery.Setup(c => c.GetCategoryById(request.category))        
                .ReturnsAsync(new Category { Id = 1, Name = "Pizzas" });

            
            //Mando el dishRequest al service
            var result = await _createDishService.CreateDish(request);

            // Assert
            Assert.Equal(request.name, result.name);
            Assert.Equal(request.price, result.price);
            Assert.Equal(request.category, result.Category.Id);
            Assert.Equal(request.description, result.description);
            Assert.Equal(request.image, result.image);
        }


        [Fact]//Crear plato con nombre vacio
        public async Task CreateDish_ShouldThrowDishNameEmptyException_WhenNameIsEmpty()  
        {

            //En el dishRequest puse que no era necesario imagen y descripcion
            var request = new DishRequest { name = "", price = 100, category = 1 };   

            //Deberia devolver la exception de nombre vacio
            await Assert.ThrowsAsync<DishNameEmptyException>(() => _createDishService.CreateDish(request));  
        }


        [Fact]//Creo un plato con precio invalid
        public async Task CreateDish_ShouldThrowDishInvalidPriceException_WhenPriceIsZeroOrNegative() 
        {
            var request = new DishRequest { name = "Pizza", price = 0, category = 1 };

            //deberia devolver exception de precio
            await Assert.ThrowsAsync<DishInvalidPriceException>(() => _createDishService.CreateDish(request)); 
        }

        [Fact]//Crear un plato con nombre repetido
        public async Task CreateDish_ShouldThrowDishAlreadyExistsException_WhenDishExists()  
        {
            var request = new DishRequest { name = "Pizza", price = 100, category = 1 };


            //Moqueo que exista el nombre
            _mockDishQuery.Setup(q => q.GetDishByName(request.name)).ReturnsAsync(true);  

            //Deberia devolver una exception de nombre repetido
            await Assert.ThrowsAsync<DishAlreadyExistsException>(() => _createDishService.CreateDish(request));
        }

        [Fact]//Crear un plato con categoria erronea
        public async Task CreateDish_ShouldThrowCategoryNotFoundException_WhenCategoryDoesNotExist()
        {
            var request = new DishRequest { name = "Pizza", price = 100, category = 999 };


            //Moqueo la categoria 999 para que no exista
            _mockCategoryQuery.Setup(c => c.GetCategoryById(request.category)).ReturnsAsync((Category)null); 


            //Deberia devolver una exception de categoria no encontrada
            await Assert.ThrowsAsync<CategoryNotFoundException>(() => _createDishService.CreateDish(request));
        }




    }
}
