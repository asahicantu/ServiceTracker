using System;
using System.Collections.Generic;

namespace ServiceTracker.DAL.Models
{
    public partial class Portfolio
    {
        public Portfolio()
        {
            SubPortfolios = new HashSet<SubPortfolio>();
        }

        public int Id { get; set; }
        public string Name { get; set; }

        public virtual ICollection<SubPortfolio> SubPortfolios { get; set; }
    }
}
