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
    public class RepairRequestsController : Controller
    {
        private ArizonaSignCompanyEntities db = new ArizonaSignCompanyEntities();

        // GET: RepairRequests
        [ChildActionOnly]
        [Authorize(Roles = "Admin")]
        public ActionResult Index()
        {
            var requestType = RequestType.repair.ToString();
            return PartialView(db.Requests.Where(r => r.Type == requestType));
        }

        // GET: RepairRequests/Details/5
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

        // GET: RepairRequests/Create
        [AllowAnonymous]
        public ActionResult Create()
        {
            return View();
        }

        // POST: RepairRequests/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(RepairRequestViewModels repair, HttpPostedFileBase upload)
        {

            var isValid = ModelState.IsValid;
            if (upload == null || upload.ContentLength == 0)
            {
                isValid = false;
                ModelState.AddModelError(null, "Please upload a valid file");
            }
            if (isValid)
            {

                var repairRequest = new Request
                {
                    first_name = repair.first_name,
                    last_name = repair.last_name,
                    description = repair.description,                  
                    contact = repair.contact,
                    location = repair.location,
                    company = repair.company,
                    Request_number = repair.Request_number,
                    Type = RequestType.repair.ToString()
                    
                };
                    var filename = DateTime.Now.ToString("yyyyMMdd-HHmmss-fffff") + "_" + upload.FileName;
                    var filepath = Server.MapPath("~/UserUploads");
                    var folderpath = Path.Combine(filepath, filename);
                    upload.SaveAs(folderpath);
                    repairRequest.attachment = filename;
                    db.Requests.Add(repairRequest);
                    db.SaveChanges();
                    return RedirectToAction("Create", "RepairRequests");
            }

            return View(repair);
        }

        // GET: RepairRequests/Edit/5
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

        // POST: RepairRequests/Edit/5
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

        // GET: RepairRequests/Delete/5
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
            return PartialView(request);
        }

        // POST: RepairRequests/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Request request = db.Requests.Find(id);
            db.Requests.Remove(request);
            db.SaveChanges();
            return RedirectToAction("Index","Dashboard");
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
