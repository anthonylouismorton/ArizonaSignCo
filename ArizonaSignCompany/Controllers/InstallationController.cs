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
    public class InstallationController : Controller
    {
        private ArizonaSignCompanyEntities db = new ArizonaSignCompanyEntities();

        // GET: Installation
        [ChildActionOnly]
        [Authorize(Roles = "Admin")]
        public ActionResult Index()
        {
            var requestType = RequestType.installation.ToString();
            return PartialView(db.Requests.Where(r => r.Type == requestType));
        }

        // GET: Installation/Details/5
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

        // GET: Installation/Create
        [AllowAnonymous]
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
        public async Task<ActionResult> Create(InstallationViewModel installation, HttpPostedFileBase upload)
        {
            var isValid = ModelState.IsValid;
            if(upload == null || upload.ContentLength == 0)
            {
                isValid = false;
                ModelState.AddModelError(null, "Please upload a valid file");
            }
            if (isValid)
            {

                var installationRequest = new Request
                {
                    Request_number = installation.Request_number,
                    first_name = installation.first_name,
                    last_name = installation.last_name,
                    company = installation.company,
                    contact = installation.contact,
                    Type = RequestType.installation.ToString()
                };              
              
                    var filename = DateTime.Now.ToString("yyyyMMdd-HHmmss-fffff") + "_" + upload.FileName;
                    var filepath = Server.MapPath("~/UserUploads");
                    var folderpath = Path.Combine(filepath, filename);
                    upload.SaveAs(folderpath);
                    installationRequest.attachment = filename;           
                    db.Requests.Add(installationRequest);
                    db.SaveChanges(); 
                    return RedirectToAction("Create","Installation");
            }

            return View(installation);
        }

        // GET: Installation/Edit/5
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

        // POST: Installation/Edit/5
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

        // GET: Installation/Delete/5
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

        // POST: Installation/Delete/5
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
