using Application.Models.Response;
using Swashbuckle.AspNetCore.Filters;

namespace Restaurant.SwaggerExamples.DishExamples.Create
{
    public class CreateDishCreatedExample : IExamplesProvider<DishResponse>
    {
        public DishResponse GetExamples()
        {
            return new DishResponse
            {
                id = Guid.Parse("123e4567-e89b-12d3-a456-426614174000"),
                name = "Pizza Margherita",
                description = "Pizza clásica con salsa de tomate, mozzarella fresca, albahaca y aceite de oliva extra virgen",
                price = 850.5m,
                Category = new GenericResponse
                {
                    Id = 1,
                    Name = "Pizzas"
                },
                image = "https://restaurant.com/images/pizza-margherita.jpg",
                isActive = true,
                createdAt = DateTime.Parse("2024-03-15T10:30:00Z"),
                updatedAt = DateTime.Parse("2024-03-15T10:30:00Z")
            };
        }
    }
}
