using Application.Models.Response;
using Swashbuckle.AspNetCore.Filters;

namespace Restaurant.SwaggerExamples.StatusExamples.Get
{
    public class GetAllStatusOKExamples : IExamplesProvider<IEnumerable<StatusResponse>>
    {
        public IEnumerable<StatusResponse> GetExamples()
        {
            return new List<StatusResponse>
            {
                new StatusResponse
                {
                    id = 1,
                    name = "Pendiente"
                },
                new StatusResponse
                {
                    id = 2,
                    name = "En preparación"
                },
                new StatusResponse
                {
                    id = 3,
                    name = "Listo"
                },
                new StatusResponse
                {
                    id = 4,
                    name = "Entregado"
                },
                new StatusResponse
                {
                    id = 5,
                    name = "Cancelado"
                }
            };
        }
    }
}
