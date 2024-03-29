﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using ArizonaSignCompany.Models;
using Microsoft.AspNet.Identity;

namespace ArizonaSignCompany.Controllers
{
    [Authorize]
    public class LiftScheduleController : Controller
    {
        private ArizonaSignCompanyEntities db = new ArizonaSignCompanyEntities();

        // GET: LiftSchedule
        [ChildActionOnly]
        
        public ActionResult Index()
        {
            var lift_Schedule = db.Lift_Schedule.Include(l => l.Customer_Information);
            return PartialView(lift_Schedule.ToList());
        }
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var request = db.Lift_Schedule.Find(id);
            if (request == null)
            {
                return HttpNotFound();
            }
            return View(request);
        }


        // GET: LiftSchedule/Create
        public ActionResult Create()
        {
            
            return View();
        }

        // POST: LiftSchedule/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(LiftScheduleViewModel schedule)
        {
            var isValid = ModelState.IsValid;
            if(!isValid && User.IsInRole("Admin"))
            {
                ModelState.Clear();
                isValid = schedule.isValidForAdmin(out var errormessage);
                if(!isValid)
                {
                    ModelState.AddModelError(null, errormessage);
                }
            }
            if (isValid)
            {
                var scheduleLift = new Lift_Schedule
                {
                    Lift_Date = schedule.Lift_Date,
                    Lift_Location = schedule.Lift_Location,
                    end_time = schedule.end_time,
                    start_time = schedule.start_time,
                    Lift_Contact = schedule.Lift_Contact,
                    Customer_ID = User.Identity.GetUserId(),
                    lift_Id = schedule.lift_id


                };
                db.Lift_Schedule.Add(scheduleLift);
                db.SaveChanges();
                return RedirectToAction("Create", "LiftSchedule");
            }
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
                return RedirectToAction("Index", "Dashboard");
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
            return RedirectToAction("Index", "Dashboard");
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
