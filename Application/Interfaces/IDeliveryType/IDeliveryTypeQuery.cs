using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces.IDeliveryType
{
    public interface IDeliveryTypeQuery
    {
        Task<List<DeliveryType>> GetAllDeliverysTypes();
        Task<DeliveryType> GetDeliveryTypeById(int id);
    }
}
