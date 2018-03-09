using IssuingInvoices.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IssuingInvoices.MEFExtensions
{
    interface IVatSetUp
    {
        double CalculateBruttoPrice(Invoice.VatCountry vatType, double nettoPrice);
    }
}
