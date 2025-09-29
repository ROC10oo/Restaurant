using Application.Interfaces.IStatus;
using Application.Models.Response;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Restaurant.SwaggerExamples.StatusExamples.Get;
using Swashbuckle.AspNetCore.Filters;

namespace Restaurant.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StatusController : ControllerBase
    {
        private readonly IGetStatusService _getStatusService;

        public StatusController(IGetStatusService getStatusService)
        {
            _getStatusService = getStatusService;
        }


        /// <summary>
        /// Obtener estados de ordenes
        /// </summary>
        /// <remarks>
        /// Obtiene todos los estados posibles para las órdenes y sus items.
        ///
        ///
        /// **Estados típicos:**
        /// - Pendiente: orden recién creada
        /// - En preparación: cocina comenzó a preparar
        /// - Listo: orden lista para entregar
        /// - Entregado: orden completada
        /// -Cancelado: orden cancelada
        /// </remarks>
        /// /// <response code="200">Lista de estados obtenida exitosamente</response>
        /// 

        [ProducesResponseType(typeof(IEnumerable<StatusResponse>), StatusCodes.Status200OK)]
        [SwaggerResponseExample(StatusCodes.Status200OK, typeof(GetAllStatusOKExamples))]
        [HttpGet]
        [Route("/api/v1/Status")]
        public async Task<ActionResult<StatusResponse>> GetStatues()
        {
            var statues = await _getStatusService.GetAllStatus();
            return Ok(statues);   //Mirar los nombres de estatus el statues y states
        }
    }
}
