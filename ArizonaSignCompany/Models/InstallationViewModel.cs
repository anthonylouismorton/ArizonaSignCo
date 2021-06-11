using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ArizonaSignCompany.Models
{
    public class InstallationViewModel
    {
        [Required]
        public int Request_number { get; set; }

        [Required]
        [Display(Name = "Installation Package")]
        public byte[] attachment { get; set; }

        [Required]
        [Display(Name = "Contact")]
        public string contact { get; set; }
    }
}