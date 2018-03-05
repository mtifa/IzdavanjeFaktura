using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace IssuingInvoices.Models
{
    public class SoldItem
    {
        public int SoldItemId { get; set; }
        public int ProductId { get; set; }
        public int InoviceId { get; set; }
        public virtual Invoice Invoice { get; set; }
        public virtual Product Product { get; set; }
    }
}