using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Models.Request
{
    public class OrderRequest
    {
        public List<OrderItemRequest> items { get; set; }
        public DeliveryRequest delivery { get; set; }
        public string notes { get; set; }
    }
}
