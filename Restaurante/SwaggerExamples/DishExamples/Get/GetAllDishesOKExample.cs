using Application.Models.Response;
using Swashbuckle.AspNetCore.Filters;

namespace Restaurant.SwaggerExamples.DishExamples.Get
{
    public class GetAllDishesOKExample : IExamplesProvider<IEnumerable<DishResponse>>
    {
        public IEnumerable<DishResponse> GetExamples()
        {
            return new List<DishResponse>
            {
                new DishResponse
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
                },
                new DishResponse
                {
                    id = Guid.Parse("987fcdeb-51a2-43d7-8f9e-123456789abc"),
                    name = "Lasagna Bolognesa",
                    description = "Lasagna tradicional con salsa bolognesa, bechamel y queso parmesano",
                    price = 950m,
                    Category = new GenericResponse
                    {
                        Id = 2,
                        Name = "Pastas"
                    },
                    image = "https://restaurant.com/images/lasagna-bolognesa.jpg",
                    isActive = true,
                    createdAt = DateTime.Parse("2024-03-15T09:15:00Z"),
                    updatedAt = DateTime.Parse("2024-03-15T09:15:00Z")
                }
            };
        }
    }
}
