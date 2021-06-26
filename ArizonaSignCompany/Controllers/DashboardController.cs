using ArizonaSignCompany.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ArizonaSignCompany.Controllers
{
    [Authorize(Roles = "Admin")]
    public class DashboardController : Controller
    {
        private ArizonaSignCompanyEntities db = new ArizonaSignCompanyEntities();
        // GET: Dashboard
        public ActionResult Index(SortDescriptor sort)
        {
            ViewBag.sortDescriptor = sort;
            return View();
        }

        [ChildActionOnly]
        public ActionResult AccountApproval(customer_informationColumnName? sortColumn, bool? sortDirection)
        {
            ViewBag.sortColumn = sortColumn;
            ViewBag.sortDirection = sortDirection;
            var approvals = db.Customer_Information.Where(c => !c.isApproved).sortByColumn(sortColumn, sortDirection);
            return PartialView(approvals.ToList());
        }

        [HttpGet]
        public ActionResult ApproveAccount(string id)
        {
            var approvals = db.Customer_Information.Find(id);

            return View(approvals);
        }

        [HttpPost]
        [ActionName("ApproveAccount")]
        [ValidateAntiForgeryToken]
        public ActionResult ApproveConfirmed(string id)
        {
            var customer = db.Customer_Information.Find(id);
            customer.isApproved = true;
            db.Entry(customer).State = System.Data.Entity.EntityState.Modified;
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        public ActionResult Archive(requestColumnName? sortColumn, bool? sortDirection)
        {
            ViewBag.sortColumn = sortColumn;
            ViewBag.sortDirection = sortDirection;
            var dbRequests = db.Requests.sortByColumn(sortColumn, sortDirection);
            return View(dbRequests);
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