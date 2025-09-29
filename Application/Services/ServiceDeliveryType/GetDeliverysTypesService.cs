using Application.Interfaces.IDeliveryType;
using Application.Models.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services.ServiceDeliveryType
{
    public class GetDeliverysTypesService : IGetDeliverysTypesService
    {
        private readonly IDeliveryTypeQuery _deliveryTypeQuery;

        public GetDeliverysTypesService(IDeliveryTypeQuery deliveryTypeQuery)
        {
            _deliveryTypeQuery = deliveryTypeQuery;
        }

        public async Task<List<DeliveryTypeResponse>> GetAllDeliverysTypes()
        {
            var deliverys = await _deliveryTypeQuery.GetAllDeliverysTypes();

            return deliverys.Select(delivery => new DeliveryTypeResponse
            {
                id = delivery.Id,
                name = delivery.Name,
            }).ToList();
        }
    }
}
