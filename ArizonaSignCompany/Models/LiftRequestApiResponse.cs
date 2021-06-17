using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ArizonaSignCompany.Models
{
    public class LiftRequestApiResponse
    {
        public string title { get; set; }
        public DateTime start { get; set; }
        public DateTime end { get; set; }
        public string url { get; set; }

        public LiftRequestApiResponse()
        {

        }
        public LiftRequestApiResponse(Lift_Schedule liftSchedule)
        {
            title = liftSchedule.Lift_Location;
            start = liftSchedule.Lift_Date + (liftSchedule.Lift_Time ?? TimeSpan.Zero);
            end = start.AddHours(1.5);
            url = $"/LiftSchedule/Details/{liftSchedule.lift_Id}";
        }
    }
    
}