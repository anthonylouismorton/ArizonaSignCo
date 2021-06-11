using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using ArizonaSignCompany.Models;

namespace ArizonaSignCompany.Controllers
{
    [Authorize]
    public class LiftScheduleController : Controller
    {
        private ArizonaSignCompanyEntities db = new ArizonaSignCompanyEntities();

        // GET: LiftSchedule
        public ActionResult Index()
        {
            var lift_Schedule = db.Lift_Schedule.Include(l => l.Customer_Information);
            return View(lift_Schedule.ToList());
        }

        // GET: LiftSchedule/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Lift_Schedule lift_Schedule = db.Lift_Schedule.Find(id);
            if (lift_Schedule == null)
            {
                return HttpNotFound();
            }
            return View(lift_Schedule);
        }

        // GET: LiftSchedule/Create
        public ActionResult Create()
        {
            ViewBag.Customer_ID = new SelectList(db.Customer_Information, "Customer_ID", "LastName");
            return View();
        }

        // POST: LiftSchedule/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(LiftScheduleViewModel schedule)
        {
            if (ModelState.IsValid)
            {
                var scheduleLift = new Lift_Schedule
                {
                    Lift_Date = schedule.Lift_Date,
                    Lift_Location = schedule.Lift_Location,
                    Lift_Time = schedule.Lift_Time,
                    Lift_Contact = schedule.Lift_Contact,
                    Customer_ID = schedule.Customer_ID,
                    lift_Id = schedule.lift_id


                };
                db.Lift_Schedule.Add(scheduleLift);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.Customer_ID = new SelectList(db.Customer_Information, "Customer_ID", "LastName", schedule.Customer_ID);
            return View(schedule);
        }

        // GET: LiftSchedule/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Lift_Schedule lift_Schedule = db.Lift_Schedule.Find(id);
            if (lift_Schedule == null)
            {
                return HttpNotFound();
            }
            ViewBag.Customer_ID = new SelectList(db.Customer_Information, "Customer_ID", "LastName", lift_Schedule.Customer_ID);
            return View(lift_Schedule);
        }

        // POST: LiftSchedule/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Lift_Date,Lift_Location,Lift_Time,Lift_Contact,Customer_ID,lift_Id")] Lift_Schedule lift_Schedule)
        {
            if (ModelState.IsValid)
            {
                db.Entry(lift_Schedule).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.Customer_ID = new SelectList(db.Customer_Information, "Customer_ID", "LastName", lift_Schedule.Customer_ID);
            return View(lift_Schedule);
        }

        // GET: LiftSchedule/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Lift_Schedule lift_Schedule = db.Lift_Schedule.Find(id);
            if (lift_Schedule == null)
            {
                return HttpNotFound();
            }
            return View(lift_Schedule);
        }

        // POST: LiftSchedule/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Lift_Schedule lift_Schedule = db.Lift_Schedule.Find(id);
            db.Lift_Schedule.Remove(lift_Schedule);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        public void Email()
        {

        }
    }
}
