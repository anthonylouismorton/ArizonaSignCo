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
        public LiftRequestApiResponse(Lift_Schedule liftSchedule, bool showPrivateInformation)
        {
            
            start = liftSchedule.Lift_Date + liftSchedule.start_time;
            end = liftSchedule.Lift_Date + liftSchedule.end_time;
            title = "-"+end.ToString("h:mmt").ToLower();
            
            if (showPrivateInformation)
            {
                url = $"/LiftSchedule/Details/{liftSchedule.lift_Id}";
                title +=" "+liftSchedule.Lift_Location;
            }
        }
    }
    
}