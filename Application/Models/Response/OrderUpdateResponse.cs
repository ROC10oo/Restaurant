﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Models.Response
{
    public class OrderUpdateReponse
    {
        public int orderNumber { get; set; }
        public double totalAmount { get; set; }
        public DateTime updateAt { get; set; }
    }
}
