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


        // 1. Crear plato exitosamente
        [Fact]
        public async Task CreateDish_Should_Add_Dish_To_Database()
        {
            // Arrange
            var context = GetDbContext(); //Crea la base de datos en la memoria
            var command = new DishCommand(context); //Creo el command que voy a probar...el command

            var dish = new Dish   //Creo el dish 
            {
                Name = "Pizza",
                Description = "Pizza con queso",
                Price = 12.5m,
                CategoryId = 3,
                ImageUrl = "https://example.com/pizza.jpg",
                Available = true,
                CreateDate = DateTime.UtcNow,
                UpdateDate = DateTime.UtcNow
            };

            // Act
            await command.CreateDish(dish);  //Le paso el dish al command

            // Assert
            context.Dishes.Should().ContainSingle(d =>
                d.Name == "Pizza" &&
                d.Description == "Pizza con queso" &&
                d.Price == 12.5m &&
                d.CategoryId == 3 &&
                d.ImageUrl == "https://example.com/pizza.jpg" &&
                d.Available == true
            );
        }


        // 2. Crear plato con campos faltantes → debe lanzar excepción
        [Fact]
        public async Task CreateDish_Should_Throw_When_Required_Fields_Missing()
        {
            var context = GetDbContext();
            var command = new DishCommand(context);

            var dish = new Dish
            {
                // Falta Name y Description → EF debería fallar
                Price = 10m,
                CategoryId = 2,
                ImageUrl = "http://img.com/invalid.jpg"
            };

            Func<Task> act = async () => await command.CreateDish(dish);

            await act.Should().ThrowAsync<DbUpdateException>();
        }


        // 3. Actualizar un plato existente exitosamente
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
                ImageUrl = "http://example.com/pizza.jpg",
                Available = true,
                CreateDate = DateTime.UtcNow,
                UpdateDate = DateTime.UtcNow
            };

            await command.CreateDish(dish);

            // modifico valores
            dish.Description = "Updated description";
            dish.Price = 15.5m;
            dish.UpdateDate = DateTime.UtcNow;

            await command.UpdateDish(dish);

            // Assert
            var updatedDish = await context.Dishes.FirstAsync(d => d.Name == "Pizza");

            updatedDish.Description.Should().Be("Updated description");
            updatedDish.Price.Should().Be(15.5m);
        }



        // 4. Actualizar un plato que no existe → debería lanzar excepción
        [Fact]
        public async Task UpdateDish_Should_Throw_When_Dish_Not_Exists()
        {
            var context = GetDbContext();
            var command = new DishCommand(context);

            var dish = new Dish
            {
                DishId = Guid.NewGuid(), // ID inexistente
                Name = "Inexistente",
                Description = "Nada",
                Price = 5m,
                CategoryId = 1,
                ImageUrl = "http://img.com/notfound.jpg",
                Available = true,
                CreateDate = DateTime.UtcNow,
                UpdateDate = DateTime.UtcNow
            };

            Func<Task> act = async () => await command.UpdateDish(dish);

            await act.Should().ThrowAsync<DbUpdateConcurrencyException>();
        }

    }
}
