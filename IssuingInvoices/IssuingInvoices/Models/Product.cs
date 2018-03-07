using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace IssuingInvoices.Models
{
    public class Product
    {
        public int ProductId { get; set; }
        [Display(Name = "Opis")]
        public string Description { get; set; }
        [Display(Name = "Količina")]
        public double Amount { get; set; }
        [Display(Name = "Jedinična cijena")]
        public double UnitPrice { get; set; }
        [Display(Name = "Ukupna cijena bez poreza")]
        public double NettoPrice
        {
            get
            {
                return Amount * UnitPrice;
            }
        }
    }
}