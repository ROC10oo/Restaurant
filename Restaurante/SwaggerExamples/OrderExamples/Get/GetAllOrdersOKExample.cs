using Application.Models.Response;
using Swashbuckle.AspNetCore.Filters;

namespace Restaurant.SwaggerExamples.OrderExamples.Get
{
    public class GetAllOrdersOKExample : IExamplesProvider<IEnumerable<OrderDetailsResponse>>
    {
        public IEnumerable<OrderDetailsResponse> GetExamples()
        {
            return new List<OrderDetailsResponse>
            {
                new OrderDetailsResponse
                {
                    orderNumber = 1001,
                    totalAmount = 1800.5,
                    deliveryTo = "Av. Corrientes 1234, Buenos Aires",
                    notes = "Timbre: Departamento 5B",
                    status = new GenericResponse
                    {
                        Id = 2,
                        Name = "En preparación"
                    },
                    deliveryType = new GenericResponse
                    {
                        Id = 1,
                        Name = "Delivery"
                    },
                    items = new List<OrderItemResponse>
                    {
                        new OrderItemResponse
                        {
                            id = 1,
                            quantity = 2,
                            notes = "Sin albahaca, por favor",
                            status = new GenericResponse
                            {
                                Id = 2,
                                Name = "En preparación"
                            },
                            dish = new DishShortResponse
                            {
                                id = Guid.Parse("123e4567-e89b-12d3-a456-426614174000"),
                                name = "Pizza Margherita",
                                image = "https://restaurant.com/images/pizza-margherita.jpg"
                            }
                        },
                        new OrderItemResponse
                        {
                            id = 2,
                            quantity = 1,
                            notes = "Bien cocida",
                            status = new GenericResponse
                            {
                                Id = 2,
                                Name = "En preparación"
                            },
                            dish = new DishShortResponse
                            {
                                id = Guid.Parse("987fcdeb-51a2-43d7-8f9e-123456789abc"),
                                name = "Lasagna Bolognesa",
                                image = "https://restaurant.com/images/lasagna-bolognesa.jpg"
                            }
                        }
                    },
                    createdAt = DateTime.Parse("2024-03-15T14:30:00Z"),
                    updatedAt = DateTime.Parse("2024-03-15T14:35:00Z")
                }
            };
        }
    }
}
