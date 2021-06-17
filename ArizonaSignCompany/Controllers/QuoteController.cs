using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using ArizonaSignCompany.Models;

namespace ArizonaSignCompany.Controllers
{
    public class QuoteController : Controller
    {
        private ArizonaSignCompanyEntities db = new ArizonaSignCompanyEntities();

        // GET: Quote
        [ChildActionOnly]
        [Authorize(Roles = "Admin")]
        public ActionResult Index()
        {
            var requestType = RequestType.quote.ToString();
            return PartialView(db.Requests.Where(r => r.Type == requestType));
        }

        // GET: Quote/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Request request = db.Requests.Find(id);
            if (request == null)
            {
                return HttpNotFound();
            }
            return View(request);
        }

        // GET: Quote/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Quote/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(QuoteViewModel quote, HttpPostedFileBase upload)
        {
            var isValid = ModelState.IsValid;
            if (upload == null || upload.ContentLength == 0)
            {
                isValid = false;
                
                    ModelState.AddModelError(null, "Invalid file type. Please select another file.");
                                
            }
            if (isValid)
            {
                var quoteRequest = new Request
                {
                    Request_number = quote.Request_number,
                    first_name = quote.first_name,
                    last_name = quote.last_name,
                    company = quote.company,
                    contact = quote.contact,
                    Type = RequestType.quote.ToString()
                };

                    var filename = DateTime.Now.ToString("yyyyMMdd-HHmmss-fffff") + "_" + upload.FileName;
                    var filepath = Server.MapPath("~/UserUploads");
                    var folderpath = Path.Combine(filepath, filename);
                    upload.SaveAs(folderpath);
                    quoteRequest.attachment = filename;
                    db.Requests.Add(quoteRequest);
                    db.SaveChanges();
                    return RedirectToAction("Create", "Quote");

            }

            return View(quote);
        }

        // GET: Quote/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Request request = db.Requests.Find(id);
            if (request == null)
            {
                return HttpNotFound();
            }
            return View(request);
        }

        // POST: Quote/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Request_number,first_name,last_name,Type,description,attachment,contact,location,company")] Request request)
        {
            if (ModelState.IsValid)
            {
                db.Entry(request).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index","Dashboard");
            }
            return View(request);
        }

        // GET: Quote/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Request request = db.Requests.Find(id);
            if (request == null)
            {
                return HttpNotFound();
            }
            return View(request);
        }

        // POST: Quote/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Request request = db.Requests.Find(id);
            db.Requests.Remove(request);
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
