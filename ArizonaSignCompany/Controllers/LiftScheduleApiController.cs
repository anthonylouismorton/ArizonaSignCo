using ArizonaSignCompany.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace ArizonaSignCompany.Controllers
{
    public class LiftScheduleApiController : ApiController
    {
        private ArizonaSignCompanyEntities db = new ArizonaSignCompanyEntities();
        [Authorize]
        public IEnumerable<LiftRequestApiResponse> Get()
        {
            var isAdmin = User.IsInRole("Admin");
            var startDate = DateTime.Today.AddDays(-DateTime.Today.Day + 1).AddMonths(-1);
            var lift = db.Lift_Schedule.Where(ls => ls.Lift_Date >= startDate)
            .AsEnumerable().Select(ls => new LiftRequestApiResponse(ls, isAdmin));
            return lift;
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
