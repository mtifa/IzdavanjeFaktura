using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace IssuingInvoices.Models
{
    public class Invoice
    {
        public int InvoiceId { get; set; }
        public DateTime DateCreated { get; set; }
        public DateTime DeliverDate { get; set; }
        public double NettoPrice { get; set; }
        public double TotalPrice { get; set; }
        public string ClientName { get; set; }
        public virtual ICollection<SoldItem> SoldItems { get; set; }
    }
}