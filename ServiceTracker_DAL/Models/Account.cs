using System;
using System.Collections.Generic;

namespace ServiceTracker.DAL.Models
{
    public partial class Account
    {
        public Account()
        {
            Service = new HashSet<Service>();
        }

        public int Id { get; set; }
        public string CountryCode { get; set; }
        public string CurrencyCode { get; set; }
        public int? ServiceDeliveryManagerId { get; set; }
        public int? AccountManagerId { get; set; }
        public string QuoteFtl { get; set; }
        public string PurchaseOrder { get; set; }
        public int? CompanyId { get; set; }
        public string Description { get; set; }
        public DateTime? Date { get; set; }

        public virtual Employee AccountManager { get; set; }
        public virtual Company Company { get; set; }
        public virtual Country CountryCodeNavigation { get; set; }
        public virtual Currency CurrencyCodeNavigation { get; set; }
        public virtual Employee ServiceDeliveryManager { get; set; }
        public virtual ICollection<Service> Service { get; set; }
    }
}
