using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ArizonaSignCompany.Models
{
    [MetadataType(typeof(liftScheduleAnnotations))]
    public partial class Lift_Schedule
    {

    }

    public class liftScheduleAnnotations
    {
        [DataType(DataType.Date)]
        [Display(Name = "Date")]
        public System.DateTime Lift_Date { get; set; }

        [Display(Name = "Location")]
        public string Lift_Location { get; set; }
        [DataType(DataType.Time)]

        [Display(Name = "Time")]
        public Nullable<System.TimeSpan> Lift_Time { get; set; }

        [Display(Name = "Contact Information")]
        public string Lift_Contact { get; set; }
        public string Customer_ID { get; set; }
        public int lift_Id { get; set; }
    }
}