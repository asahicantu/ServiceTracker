﻿using System;
using System.Collections.Generic;

namespace ServiceTracker.DAL.Models
{
    public partial class Product
    {
        public int Id { get; set; }
        public string MasterCode { get; set; }
        public string Name { get; set; }
    }
}
