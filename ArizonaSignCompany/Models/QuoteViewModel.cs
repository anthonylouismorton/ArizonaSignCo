using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ArizonaSignCompany.Models
{
    public class QuoteViewModel

    {
        [Required]
        public int Request_number { get; set; }

        [Required]
        [Display(Name = "Attachments")]
        public byte[] attachment { get; set; }

        [Required]
        [Display(Name = "Contact")]
        public string contact { get; set; }
    }
}