using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Entities;
using Infrastructure.Command;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Xunit;
using FluentAssertions;
using System.Threading.Tasks;

namespace TestProject.Command
{
    public class DishCommandTest  //Command usa DbContext asique no es necesario usar moq
    {
        private RestauranteDbContext GetDbContext()
        {
            var options = new DbContextOptionsBuilder<RestauranteDbContext>() //Se inMemory porque esto indica que no es un base real sino una base en la memoria, 
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()) //Esto hace que se cree una base limpia y vacia, para todos los test se usa una base diferente
                .Options;

            return new RestauranteDbContext(options);
        }

        [Fact]
        public async Task CreateDish_Should_Add_Dish_To_Database()
        {
            // Arrange
            var context = GetDbContext(); //Crea la base de datos en la memoria
            var command = new DishCommand(context); //Creo el command que voy a probar...el command

            var dish = new Dish   //Creo el dish 
            {
                Name = "Pizza",
                Description = "",
                Price = 12.5m,
                CategoryId = 3,
                ImageUrl = "dfsdfdsf"
            };

            // Act
            await command.CreateDish(dish);  //Le paso el dish al command

            // Assert
            context.Dishes.Should().ContainSingle(d => d.Name == "Pizza" && d.Description == "" && d.Price == 12.5m && d.CategoryId == 3 && d.ImageUrl == "dfsdfdsf");
        }



        [Fact]
        public async Task UpdateDish_Should_Update_Dish_In_Database()
        {
            var context = GetDbContext();
            var command = new DishCommand(context);

            var dish = new Dish
            {
                Name = "Pizza",
                Description = "Original description",
                Price = 10.0m,
                CategoryId = 1,
                ImageUrl = "http://example.com/pizza.jpg"
            };

            await command.CreateDish(dish);

            // modifico valores
            dish.Description = "Updated description";
            dish.Price = 15.5m;

            
            await command.UpdateDish(dish);

            // Assert
            var updatedDish = await context.Dishes.FirstAsync(d => d.Name == "Pizza");

            updatedDish.Description.Should().Be("Updated description");
            updatedDish.Price.Should().Be(15.5m);
        }

    }
}
