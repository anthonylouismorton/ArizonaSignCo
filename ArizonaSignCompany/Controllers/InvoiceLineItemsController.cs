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
    public class InvoiceLineItemsController : Controller
    {
        private ArizonaSignCompanyEntities db = new ArizonaSignCompanyEntities();

        // GET: InvoiceLineItems
        public ActionResult Index(int id)
        {
            var invoice = db.Invoices.Find(id);
            ViewBag.invoiceid = id;
            return PartialView(invoice.InvoiceLineItems.ToList());
        }

        // GET: InvoiceLineItems/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            InvoiceLineItem invoiceLineItem = db.InvoiceLineItems.Find(id);
            if (invoiceLineItem == null)
            {
                return HttpNotFound();
            }
            return View(invoiceLineItem);
        }

        // GET: InvoiceLineItems/Create
        public ActionResult Create(int id)
        {
            
            var invoice = db.Invoices.Find(id);
            var lineitem = new InvoiceLineItem { Invoice_ID = id, Invoice = invoice };
            return View(lineitem);
        }

        // POST: InvoiceLineItems/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "LineItem_ID,description,cost,Invoice_ID,taxPercent")] InvoiceLineItem invoiceLineItem)
        {
            if (ModelState.IsValid)
            {
                invoiceLineItem.taxPercent /= 100m;
                db.InvoiceLineItems.Add(invoiceLineItem);
                db.SaveChanges();
                return RedirectToAction("Edit","Invoices", new {id = invoiceLineItem.Invoice_ID });
            }

            ViewBag.Invoice_ID = new SelectList(db.Invoices, "Invoice_ID", "description", invoiceLineItem.Invoice_ID);
            invoiceLineItem.Invoice = db.Invoices.Find(invoiceLineItem.Invoice_ID);
            return View(invoiceLineItem);
        }

        // GET: InvoiceLineItems/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            InvoiceLineItem invoiceLineItem = db.InvoiceLineItems.Find(id);
            if (invoiceLineItem == null)
            {
                return HttpNotFound();
            }
            ViewBag.Invoice_ID = new SelectList(db.Invoices, "Invoice_ID", "description", invoiceLineItem.Invoice_ID);
            return View(invoiceLineItem);
        }

        // POST: InvoiceLineItems/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "LineItem_ID,description,cost,Invoice_ID,taxPercent")] InvoiceLineItem invoiceLineItem)
        {
            if (ModelState.IsValid)
            {
                invoiceLineItem.taxPercent /= 100m;
                db.Entry(invoiceLineItem).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Edit","Invoices", new {id = invoiceLineItem.Invoice_ID });
            }
            ViewBag.Invoice_ID = new SelectList(db.Invoices, "Invoice_ID", "description", invoiceLineItem.Invoice_ID);
            return View(invoiceLineItem);
        }

        // GET: InvoiceLineItems/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            InvoiceLineItem invoiceLineItem = db.InvoiceLineItems.Find(id);
            if (invoiceLineItem == null)
            {
                return HttpNotFound();
            }
            return View(invoiceLineItem);
        }

        // POST: InvoiceLineItems/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            InvoiceLineItem invoiceLineItem = db.InvoiceLineItems.Find(id);
            db.InvoiceLineItems.Remove(invoiceLineItem);
            db.SaveChanges();
            return RedirectToAction("Edit","Invoices", new {id = invoiceLineItem.Invoice_ID });
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
