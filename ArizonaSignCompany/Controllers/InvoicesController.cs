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
    public class InvoicesController : Controller
    {
        private ArizonaSignCompanyEntities db = new ArizonaSignCompanyEntities();

        // GET: Invoices
        public ActionResult Index(string id)
        {
            var customer = db.Customer_Information.Find(id);
            return View(customer.Invoices.ToList());
           
        }

        // GET: Invoices/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Invoice invoice = db.Invoices.Find(id);
            if (invoice == null)
            {
                return HttpNotFound();
            }
            return View(invoice);
        }

        // GET: Invoices/Create
        public ActionResult Create(string id)
        {
            
            ViewBag.Customer_ID = new SelectList(db.Customer_Information
                .OrderBy(c => c.LastName).ThenBy(c => c.FirstName)
                .ThenBy(c => c.Company), "Customer_ID", "fullCustomerDetails",id);
            return View();
        }

        // POST: Invoices/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Invoice_ID,description,Customer_ID")] Invoice invoice)
        {
            if (ModelState.IsValid)
            {
                db.Invoices.Add(invoice);
                db.SaveChanges();
                return RedirectToAction("Edit", new {id = invoice.Invoice_ID });
            }

            ViewBag.Customer_ID = new SelectList(db.Customer_Information.OrderBy(c => c.LastName).ThenBy(c => c.FirstName).ThenBy(c => c.Company), "Customer_ID", "fullCustomerDetails", invoice.Customer_ID);
            return View(invoice);
        }

        // GET: Invoices/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Invoice invoice = db.Invoices.Find(id);
            if (invoice == null)
            {
                return HttpNotFound();
            }
            ViewBag.Customer_ID = new SelectList(db.Customer_Information, "Customer_ID", "LastName", invoice.Customer_ID);
            return View(invoice);
        }

        // POST: Invoices/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Invoice_ID,sub_total,total,description,Customer_ID")] Invoice invoice)
        {
            if (ModelState.IsValid)
            {
                db.Entry(invoice).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index",new {id=invoice.Customer_ID });
            }
            ViewBag.Customer_ID = new SelectList(db.Customer_Information, "Customer_ID", "LastName", invoice.Customer_ID);
            return View(invoice);
        }

        // GET: Invoices/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Invoice invoice = db.Invoices.Find(id);
            if (invoice == null)
            {
                return HttpNotFound();
            }
            return View(invoice);
        }

        // POST: Invoices/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Invoice invoice = db.Invoices.Find(id);
            var customerId = invoice.Customer_ID;
            foreach(var lineItem in invoice.InvoiceLineItems.ToList())
            {
                db.InvoiceLineItems.Remove(lineItem);
            }
            /*invoice.InvoiceLineItems.Clear();*/
            db.Invoices.Remove(invoice);
            db.SaveChanges();
            return RedirectToAction("Index", new {id=customerId });
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
