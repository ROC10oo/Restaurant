using Application.Models.Response;
using Swashbuckle.AspNetCore.Filters;

namespace Restaurant.SwaggerExamples.DeliveryTypeExamples.Get
{
    public class GetAllDeliverysOKExample : IExamplesProvider<IEnumerable<DeliveryTypeResponse>>
    {
        public IEnumerable<DeliveryTypeResponse> GetExamples()
        {
            return new List<DeliveryTypeResponse>
            {
                new DeliveryTypeResponse
                {
                    id = 1,
                    name = "Delivery",
                },
                new DeliveryTypeResponse
                {
                    id = 2,
                    name = "Retiro en local"
                }, 
                new DeliveryTypeResponse
                {
                    id = 3,
                    name = "Comida en el local"
                }
            };
        }
    }
}
