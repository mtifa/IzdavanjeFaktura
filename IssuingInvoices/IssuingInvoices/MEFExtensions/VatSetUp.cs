using IssuingInvoices.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Web;

namespace IssuingInvoices.MEFExtensions
{
    [Export(typeof(IVatSetUp))]
    public class VatSetUp : IVatSetUp
    {
        public double CalculateBruttoPrice(Invoice.VatCountry vatType, double nettoPrice)
        {
            switch (vatType)
            {
                case Invoice.VatCountry.Croatia:
                    return CalculateVat(0.25, nettoPrice);
                case Invoice.VatCountry.BiH:
                    return CalculateVat(0.17, nettoPrice);
                case Invoice.VatCountry.Serbia:
                    return CalculateVat(0.20, nettoPrice);

                default:
                    throw new ArgumentException(nameof(vatType));
            }
        }
        public static double CalculateVat(double priceWithoutVat, double VatValue)
        {
            return priceWithoutVat / (1 - VatValue);
        }
    }
}