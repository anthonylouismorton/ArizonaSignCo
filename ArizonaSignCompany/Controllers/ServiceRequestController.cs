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
    public class ServiceRequestController : Controller
    {
        private ArizonaSignCompanyEntities db = new ArizonaSignCompanyEntities();

        // GET: ServiceRequest
        [ChildActionOnly]
        [Authorize(Roles = "Admin")]
        public ActionResult Index()
        {
            var requestType = RequestType.service.ToString();
            return PartialView(db.Requests.Where(r => r.Type == requestType));
        }

        // GET: ServiceRequest/Details/5
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

        // GET: ServiceRequest/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Installation/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(ServiceRequestViewModel service, HttpPostedFileBase upload)
        {

                var isValid = ModelState.IsValid;
                if (upload == null || upload.ContentLength == 0)
                {
                    isValid = false;
                    ModelState.AddModelError(null, "Invalid file type. Please select another file.");

                }
                if (isValid)
                {
                    var serviceRequest = new Request
                    {
                        first_name = service.first_name,
                        last_name = service.last_name,
                        description = service.description,
                        contact = service.contact,
                        location = service.location,
                        company = service.company,
                        Request_number = service.Request_number,
                        Type = RequestType.service.ToString()



                    };

                        var filename = DateTime.Now.ToString("yyyyMMdd-HHmmss-fffff") + "_" + upload.FileName;
                        var filepath = Server.MapPath("~/UserUploads");
                        var folderpath = Path.Combine(filepath, filename);
                        upload.SaveAs(folderpath);
                        serviceRequest.attachment = filename;
                        db.Requests.Add(serviceRequest);
                        db.SaveChanges();
                        return RedirectToAction("Create", "ServiceRequest");
                }

                return View(service);
           
        }

        // GET: ServiceRequest/Edit/5
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

        // POST: ServiceRequest/Edit/5
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

        // GET: ServiceRequest/Delete/5
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

        // POST: ServiceRequest/Delete/5
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
