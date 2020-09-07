using System;
using System.Collections.Generic;

namespace ServiceTracker.DAL.Models
{
    public partial class Company
    {
        public Company()
        {
            Account = new HashSet<Account>();
        }

        public int Id { get; set; }
        public string Name { get; set; }

        public virtual ICollection<Account> Account { get; set; }
    }
}
