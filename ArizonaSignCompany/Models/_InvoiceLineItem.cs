using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ArizonaSignCompany.Models
{
    [MetadataType(typeof(InvoiceLineItemAnnotations))]
    public partial class InvoiceLineItem
    {
        [DataType(DataType.Currency)]
        public decimal taxableItem => cost * ((taxPercent ?? 0m) + 1m);
    }
    public class InvoiceLineItemAnnotations
    {
        public int LineItem_ID { get; set; }
        public string description { get; set; }

        [DataType(DataType.Currency)]
        public decimal cost { get; set; }
        public int Invoice_ID { get; set; }

        [DisplayFormat(DataFormatString = "{0:P2}", ApplyFormatInEditMode = false)]
        public Nullable<decimal> taxPercent { get; set; }
    }
}