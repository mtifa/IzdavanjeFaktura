using IssuingInvoices.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace IssuingInvoices.Models
{
    public class Invoice
    {
        public int InvoiceId { get; set; }
        [Display(Name = "Datum stvaranja")]
        public DateTime DateCreated { get; set; }
        [Display(Name = "Datum dospijeća")]
        public DateTime DeliverDate { get; set; }
        [Display(Name = "Ukupna cijena bez poreza")]
        [DataType(DataType.Currency)]
        public double Netto
        {
            get
            {
                return Products.Sum(i => i.NettoPrice);
            }
        }
        [Display(Name = "Država PDV-a")]
        public VatCountry Vat { get; set; }
        [Display(Name = "Ukupna cijena s PDV-om")]
        [DataType(DataType.Currency)]
        public double TotalPrice { get; set; }
        [Display(Name = "Klijent")]
        public string ClientName { get; set; }
        public virtual ICollection<Product> Products { get; set; }
        [Display(Name = "Stvaratelj računa")]
        public ApplicationUser User { get; set; }

        public enum VatCountry
        {
            Croatia, BiH, Serbia
        }
    }
}