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
        [Display(Name = "First Name")]
        public string first_name { get; set; }

        [Required]
        [Display(Name = "Last Name")]
        public string last_name { get; set; }

        [Required]
        [StringLength(4000, ErrorMessage = "The Desciption cannot exceed 4000 characters", MinimumLength = 1)]
        [Display(Name = "Description")]
        public string description { get; set; }

        [Required]
        [Display(Name = "Contact")]
        public string contact { get; set; }

        [Required]
        [Display(Name = "Company")]
        public string company { get; set; }
        [Required]
        [Display(Name = "Location")]
        public string location { get; set; }
    }
}