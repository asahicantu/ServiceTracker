using System;
using System.Collections.Generic;

namespace ServiceTracker.DAL.Models
{
    public partial class CostType
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string GLAccount { get; set; }
    }
}
