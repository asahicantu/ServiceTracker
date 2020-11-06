using System;
using System.Collections.Generic;

namespace ServiceTracker.DAL.Models
{
    public partial class Service
    {
        public int Id { get; set; }
        public int? DomainId { get; set; }
        public int? AccountId { get; set; }
        public string Country { get; set; }
        public string Sdl { get; set; }
        public string Am { get; set; }
        public DateTime? Date { get; set; }
        public string QuoteFtl { get; set; }
        public string Po { get; set; }
        public string Client { get; set; }
        public string Field { get; set; }
        public string Well { get; set; }
        public string BL { get; set; }
        public string Au { get; set; }
        public string Ac { get; set; }
        public string Portfolio { get; set; }
        public string SubPortfolio { get; set; }
        public string MasterCode { get; set; }
        public string Currency { get; set; }
        public string Fxrate { get; set; }
        public string Comment { get; set; }
        public string TechnicalLead { get; set; }
        public string ChangePointTask { get; set; }
        public decimal? Rofo { get; set; }
        public decimal? IMf { get; set; }
        public decimal? Mmf { get; set; }
        public decimal? SentToInvoice { get; set; }
        public decimal? Revenue { get; set; }
        public string InvocieNumber { get; set; }
        public decimal? Cost { get; set; }
        public decimal? CostReceived { get; set; }
        public string CostType { get; set; }
        public string Glaccount { get; set; }
        public string CostDescription { get; set; }
        public bool? Blocked { get; set; }

        public virtual Account Account { get; set; }
        public virtual Domain Domain { get; set; }
    }
}
