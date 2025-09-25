using Application.Enums;
using Domain.Entities;
using FluentAssertions;
using Infrastructure.Data;
using Infrastructure.Query;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestProject.Query
{
    public class DishQueryTest
    {
        private RestauranteDbContext GetDbContext()
        {
            var options = new DbContextOptionsBuilder<RestauranteDbContext>() //Se inMemory porque esto indica que no es un base real sino una base en la memoria, 
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()) //Esto hace que se cree una base limpia y vacia, para todos los test se usa una base diferente
                .Options;

            return new RestauranteDbContext(options);
        }



        //Agrego los datos a la base
        private async Task SeedData(RestauranteDbContext context)
        {
            var dishes = new List<Dish>
            {
                new Dish
                {
                    DishId = Guid.NewGuid(),
                    Name = "Pizza",
                    Description = "Cheese pizza",
                    Price = 10.0m,
                    Available = true,
                    CategoryId = 1,
                    ImageUrl = "url",
                    CreateDate = DateTime.UtcNow,
                    UpdateDate = DateTime.UtcNow
                },
                new Dish
                {
                    DishId = Guid.NewGuid(),
                    Name = "Burger",
                    Description = "Beef burger",
                    Price = 8.0m,
                    Available = false,
                    CategoryId = 2,
                    ImageUrl = "url",
                    CreateDate = DateTime.UtcNow,
                    UpdateDate = DateTime.UtcNow
                },
                new Dish
                {
                    DishId = Guid.NewGuid(),
                    Name = "Salad",
                    Description = "Fresh salad",
                    Price = 5.0m,
                    Available = true,
                    CategoryId = 2,
                    ImageUrl = "url",
                    CreateDate = DateTime.UtcNow,
                    UpdateDate = DateTime.UtcNow
                }
            };

            await context.Dishes.AddRangeAsync(dishes);
            await context.SaveChangesAsync();
        }

        // 1. GetDishById - devuelve un plato existente
        [Fact]
        public async Task GetDishById_Should_Return_Dish_When_Exists()
        {
            var context = GetDbContext();
            await SeedData(context);
            var query = new DishQuery(context);

            var existingDish = await context.Dishes.FirstAsync();

            var result = await query.GetDishById(existingDish.DishId);

            result.Should().NotBeNull();
            result.Name.Should().Be(existingDish.Name);
        }

        // 2. GetDishById - devuelve null si no existe
        [Fact]
        public async Task GetDishById_Should_Return_Null_When_NotFound()
        {
            var context = GetDbContext();
            await SeedData(context);
            var query = new DishQuery(context);

            var result = await query.GetDishById(Guid.NewGuid());

            result.Should().BeNull();
        }

        // 3. GetDishByName - true si existe
        [Fact]
        public async Task GetDishByName_Should_Return_True_When_Exists()
        {
            var context = GetDbContext();
            await SeedData(context);
            var query = new DishQuery(context);

            var result = await query.GetDishByName("Pizza");

            result.Should().BeTrue();
        }

        // 4. GetDishByName - false si no existe
        [Fact]
        public async Task GetDishByName_Should_Return_False_When_NotExists()
        {
            var context = GetDbContext();
            await SeedData(context);
            var query = new DishQuery(context);

            var result = await query.GetDishByName("Pasta");

            result.Should().BeFalse();
        }

        // 5. GetDishes - sin filtros
        [Fact]
        public async Task GetDishes_Should_Return_All_Dishes_When_No_Filters()
        {
            var context = GetDbContext();
            await SeedData(context);
            var query = new DishQuery(context);

            var result = await query.GetDishes();

            result.Should().HaveCount(3);
        }

        // 6. GetDishes - filtra por nombre
        [Fact]
        public async Task GetDishes_Should_Filter_By_Name()
        {
            var context = GetDbContext();
            await SeedData(context);
            var query = new DishQuery(context);

            var result = await query.GetDishes(name: "Pizza");

            result.Should().ContainSingle(d => d.Name == "Pizza");
        }

        // 7. GetDishes - filtra por categoría
        [Fact]
        public async Task GetDishes_Should_Filter_By_Category()
        {
            var context = GetDbContext();
            await SeedData(context);
            var query = new DishQuery(context);

            var result = await query.GetDishes(category: 2);

            result.Should().OnlyContain(d => d.CategoryId == 2);
        }

        // 8. GetDishes - orden ascendente por precio
        [Fact]
        public async Task GetDishes_Should_Order_By_Price_Asc()
        {
            var context = GetDbContext();
            await SeedData(context);
            var query = new DishQuery(context);

            var result = await query.GetDishes(sortByPrice: OrderByPrice.asc);

            result.Select(d => d.Price).Should().BeInAscendingOrder();
        }

        // 9. GetDishes - orden descendente por precio
        [Fact]
        public async Task GetDishes_Should_Order_By_Price_Desc()
        {
            var context = GetDbContext();
            await SeedData(context);
            var query = new DishQuery(context);

            var result = await query.GetDishes(sortByPrice: OrderByPrice.desc);

            result.Select(d => d.Price).Should().BeInDescendingOrder();
        }

        // 10. GetDishes - solo disponibles
        [Fact]
        public async Task GetDishes_Should_Filter_Only_Active()
        {
            var context = GetDbContext();
            await SeedData(context);
            var query = new DishQuery(context);

            var result = await query.GetDishes(onlyActive: true);

            result.Should().OnlyContain(d => d.Available);
        }



    }
}
