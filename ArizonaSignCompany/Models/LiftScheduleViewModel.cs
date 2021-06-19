using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ArizonaSignCompany.Models
{
    public class LiftScheduleViewModel: IValidatableObject
    {
        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            
            var results = new List<ValidationResult>();
            if (Lift_Date.DayOfWeek == DayOfWeek.Sunday || Lift_Date.DayOfWeek == DayOfWeek.Saturday)
            {
                var error = new ValidationResult("You cannot schedule a lift on a weekend. Visit the contact page for more information", new[] { nameof(Lift_Date) });
                results.Add(error);
            }
            if (Lift_Date < DateTime.Today.AddDays(1))
            {
                var error = new ValidationResult("You cannot schedule a lift early than tomorrow. Visit the contact page for more information", new[] { nameof(Lift_Date) });
                results.Add(error);
            }
            if (Lift_Date > DateTime.Today.AddMonths(2))
            {
                var error = new ValidationResult("You cannot schedule lifts further out than 2 months. Visit the contact page for more information", new[] { nameof(Lift_Date) });
                results.Add(error);
            }
            if (end_time.TotalHours > 17)
            {
                var error = new ValidationResult("You cannot schedule lifts outside of business hours. Visit the contact page for more information", new[] { nameof(end_time) });
                results.Add(error);
            }
            if(start_time.Hours < 8)
            {
                var error = new ValidationResult("You cannot schedule lifts outside of business hours. Visit the contact page for more information", new[] { nameof(start_time) });
                results.Add(error);
            }
            if (end_time - start_time < TimeSpan.FromHours(1))
            {
                var error = new ValidationResult("Your start must be before your end time. Minimum lift time is 1 hour. Visit the contact page for more information", new[] { nameof(end_time) });
                results.Add(error);
            }
            using (var db = new ArizonaSignCompanyEntities())
            {
                var schedules = db.Lift_Schedule;
                var matchingDate = schedules.Where(ls => ls.Lift_Date == Lift_Date).ToList();
                var otherLifts = matchingDate.Where(ls => !(ls.start_time > end_time || ls.end_time < start_time));
                if (otherLifts.Any()) 
                {
                    var error = new ValidationResult("You cannot schedule a lift at the same time as another lift. Visit the contact page for more information");
                    results.Add(error);
                }
            }

                return results;
        }

        public bool isValidForAdmin(out string errorMessage)
        {
            errorMessage = "";
            var isValid = true;
           if(Lift_Date == default(DateTime))
            {
                errorMessage += "Lift Date is required. ";
                isValid = false;

            }

            if (string.IsNullOrWhiteSpace(Lift_Location))
            {
                errorMessage += "Lift location is required. ";
                isValid = false;
            }

            if (string.IsNullOrWhiteSpace(Lift_Contact))
            {
                errorMessage += "Lift contact is required. ";
                isValid = false;
            }

            if (start_time == default(TimeSpan))
            {
                errorMessage += "Lift start time is required. ";
                isValid = false;

            }

            if (end_time == default(TimeSpan))
            {
                errorMessage += "Lift end time is required. ";
                isValid = false;

            }
            return isValid;
        }
        [Required]
        public int lift_id { get; set; }

        [Required]
        [DataType(DataType.Date)]
        [Display(Name = "Date")]
        public System.DateTime Lift_Date { get; set; }

        [Required]
        [Display(Name = "Location")]
        public string Lift_Location{ get; set; }

        [Required]
        [Display(Name = "Start Time")]
        [DataType(DataType.Time)]
        public TimeSpan start_time { get; set; }

        [Required]
        [Display(Name = "End Time")]
        [DataType(DataType.Time)]
        public TimeSpan end_time { get; set; }

        [Required]
        [Display(Name = "Lift Contact")]
        public string Lift_Contact { get; set; }

    }
}