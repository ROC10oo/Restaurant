using Application.Interfaces.IDeliveryType;
using Application.Models.Response;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Restaurant.SwaggerExamples.DeliveryTypeExamples.Get;
using Swashbuckle.AspNetCore.Filters;

namespace Restaurant.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DeliveryTypeController : ControllerBase
    {
        private readonly IGetDeliverysTypesService _getDeliverysTypesService;

        public DeliveryTypeController(IGetDeliverysTypesService getDeliverysTypesService)
        {
            _getDeliverysTypesService = getDeliverysTypesService;
        }

        /// <summary>
        /// Obtener tipos de entrega
        /// </summary>
        /// <remarks>
        /// Obtiene todos los tipos de entrega disponibles para las órdenes.
        ///
        ///
        /// **Casos de uso:**
        /// - Mostrar opciones de entrega al cliente durante el pedido
        /// - Configurar diferentes métodos de entrega
        /// - Calcular costos de envío según el tipo
        /// </remarks>
        /// /// <response code="200">Lista de tipos de entrega obtenida exitosamente</response>
        [ProducesResponseType(typeof(IEnumerable<DeliveryTypeResponse>), StatusCodes.Status200OK)]
        [SwaggerResponseExample(StatusCodes.Status200OK, typeof(GetAllDeliverysOKExample))]
        [HttpGet]
        [Route("/api/v1/DeliveryType")]
        public async Task<ActionResult<DeliveryTypeResponse>> GetDeliverys()
        {
            var deliverys = await _getDeliverysTypesService.GetAllDeliverysTypes();
            return Ok(deliverys);
        }

    }
}
