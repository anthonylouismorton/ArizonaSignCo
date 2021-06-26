using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ArizonaSignCompany.Models
{
    public partial class Invoice
    {
        [DataType(DataType.Currency)]
        public decimal sub_total => InvoiceLineItems.Sum(l => l.cost);

        [DataType(DataType.Currency)]
        public decimal total => InvoiceLineItems.Sum(l => l.taxableItem);
    }
}