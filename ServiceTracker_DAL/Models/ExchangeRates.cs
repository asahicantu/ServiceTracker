using System;
using System.Collections.Generic;

namespace ServiceTracker.DAL.Models
{
    public partial class ExchangeRates
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public string FromCurrencyId { get; set; }
        public string ToCurrencyId { get; set; }
        public decimal Rate { get; set; }
    }
}
