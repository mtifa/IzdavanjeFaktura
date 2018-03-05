using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace IssuingInvoices.Models
{
    public class Product
    {
        public int ProductId { get; set; }
        public string Description { get; set; }
        public double Amount { get; set; }
        public double UnitPrice { get; set; }
        public double NettoPrice { get; set; }
        public virtual ICollection<SoldItem> SoldItems { get; set; }
    }
}