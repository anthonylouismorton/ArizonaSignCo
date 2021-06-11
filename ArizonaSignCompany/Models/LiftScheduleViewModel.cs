using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ArizonaSignCompany.Models
{
    public class LiftScheduleViewModel
    {
        [Required]
        public int lift_id { get; set; }
        [Required]
        public string Customer_ID { get; set; }

        [Required]
        [Display(Name = "Date")]
        public System.DateTime Lift_Date { get; set; }

        [Required]
        [Display(Name = "Location")]
        public string Lift_Location{ get; set; }

        [Required]
        [Display(Name = "Time")]
        public string Lift_Time { get; set; }

        [Required]
        [Display(Name = "Contact")]
        public string Lift_Contact { get; set; }

    }
}