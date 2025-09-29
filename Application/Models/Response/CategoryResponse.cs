﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Models.Response
{
    public class CategoryResponse
    {
        public int id { get; set; }
        public string name { get; set; }
        public string description { get; set; }
        public int order { get; set; }
    }
}
