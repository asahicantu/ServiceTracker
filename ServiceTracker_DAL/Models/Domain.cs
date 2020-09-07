using System;
using System.Collections.Generic;

namespace ServiceTracker.DAL.Models
{
    public partial class Domain
    {
        public Domain()
        {
            Service = new HashSet<Service>();
        }

        public int Id { get; set; }
        public int? AccountUnitId { get; set; }
        public int? ActivityCostId { get; set; }
        public int? PortfolioId { get; set; }
        public int? SubPortfolioId { get; set; }

        public virtual ICollection<Service> Service { get; set; }
    }
}
