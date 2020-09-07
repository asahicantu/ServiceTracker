using System;
using System.Collections.Generic;

namespace ServiceTracker.DAL.Models
{
    public partial class Employee
    {
        public Employee()
        {
            AccountAccountManager = new HashSet<Account>();
            AccountServiceDeliveryManager = new HashSet<Account>();
        }

        public int Id { get; set; }
        public string Ldap { get; set; }
        public string Name { get; set; }
        public string LastName { get; set; }
        public string Alias { get; set; }
        public string FullName1 { get; set; }
        public string FullName { get; set; }
        public bool? IsAccountManager { get; set; }
        public bool? IsServiceDeliveryManager { get; set; }
        public bool? IsEngineer { get; set; }

        public virtual ICollection<Account> AccountAccountManager { get; set; }
        public virtual ICollection<Account> AccountServiceDeliveryManager { get; set; }
    }
}
