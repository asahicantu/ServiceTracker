using System;
using System.Collections.Generic;

namespace ServiceTracker.DAL.Models
{
    public partial class Country
    {
        public Country()
        {
            Account = new HashSet<Account>();
        }

        public string Code { get; set; }
        public string Name { get; set; }
        public string CurrencyId { get; set; }

        public virtual ICollection<Account> Account { get; set; }
    }
}
