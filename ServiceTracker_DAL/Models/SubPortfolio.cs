using System;
using System.Collections.Generic;

namespace ServiceTracker.DAL.Models
{
    public partial class SubPortfolio
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public int PortfolioId { get; set; }
        public virtual Portfolio Portfolio { get; set; }
    }
}
