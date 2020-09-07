using System;
using System.Collections.Generic;

namespace ServiceTracker.DAL.Models
{
    public partial class Currency
    {
        public Currency()
        {
            Account = new HashSet<Account>();
        }

        public string Code { get; set; }
        public string Name { get; set; }

        public virtual ICollection<Account> Account { get; set; }
    }
}
