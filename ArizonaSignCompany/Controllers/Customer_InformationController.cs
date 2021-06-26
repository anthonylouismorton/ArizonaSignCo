using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using ArizonaSignCompany.Models;

namespace ArizonaSignCompany.Controllers
{
    public class Customer_InformationController : Controller
    {
        private ArizonaSignCompanyEntities db = new ArizonaSignCompanyEntities();

        // GET: Customer_Information
        public ActionResult Index(string search)
        {
            IQueryable<Customer_Information> customer_Information = db.Customer_Information;
            if(!string.IsNullOrWhiteSpace(search))
            {
                customer_Information = customer_Information.Where(c => c.LastName.Contains(search));
            }
            return View(customer_Information.ToList());
        }

        // GET: Customer_Information/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Customer_Information customer_Information = db.Customer_Information.Find(id);
            if (customer_Information == null)
            {
                return HttpNotFound();
            }
            return View(customer_Information);
        }       

        // GET: Customer_Information/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Customer_Information customer_Information = db.Customer_Information.Find(id);
            if (customer_Information == null)
            {
                return HttpNotFound();
            }
            return View(customer_Information);
        }

        // POST: Customer_Information/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "LastName,FirstName,Company,Address,City,State,Zip,Phone,Email,Customer_ID,isApproved")] Customer_Information customer_Information)
        {
            if (ModelState.IsValid)
            {
                db.Entry(customer_Information).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(customer_Information);
        }

        // GET: Customer_Information/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Customer_Information customer_Information = db.Customer_Information.Find(id);
            if (customer_Information == null)
            {
                return HttpNotFound();
            }
            return View(customer_Information);
        }

        // POST: Customer_Information/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            Customer_Information customer_Information = db.Customer_Information.Find(id);
            db.Customer_Information.Remove(customer_Information);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        public ActionResult CreateInvoice(string id)
        {
            Customer_Information customer_Information = db.Customer_Information.Find(id);
            return View(customer_Information);
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
