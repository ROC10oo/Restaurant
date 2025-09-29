﻿using Application.Models.Request;
using Application.Models.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces.IOrder
{
    public interface IUpdateOrderItemForStatusService
    {
        Task<OrderUpdateReponse> UpdateItemStatus(long orderId, int itemId, OrderItemUpdateRequest request);
    }
}
