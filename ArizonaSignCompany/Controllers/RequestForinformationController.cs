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
    public class RequestForinformationController : Controller
    {
        private ArizonaSignCompanyEntities db = new ArizonaSignCompanyEntities();

        // GET: RequestForinformation
        [ChildActionOnly]
        [Authorize(Roles = "Admin")]
        public ActionResult Index(requestColumnName? sortColumn, bool? sortDirection)
        {
            ViewBag.sortColumn = sortColumn;
            ViewBag.sortDirection = sortDirection;
            var requestType = RequestType.information.ToString();
            var dbRequests = db.Requests.Where(r => r.Type == requestType).sortByColumn(sortColumn, sortDirection);
            return PartialView(dbRequests);
        }

        // GET: RequestForinformation/Details/5
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

        // GET: RequestForinformation/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: RequestForinformation/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(RequestForInformationViewModel information, HttpPostedFileBase upload)
        {

            var isValid = ModelState.IsValid;
            if (isValid)
            {


                var informationRequest = new Request
                {
                    first_name = information.first_name,
                    last_name = information.last_name,
                    description = information.description,
                    contact = information.contact,
                    location = information.location,
                    company = information.company,
                    Request_number = information.Request_number,
                    Type = RequestType.information.ToString()



                };
                if (upload != null && upload.ContentLength > 0)
                {
                    var filename = DateTime.Now.ToString("yyyyMMdd-HHmmss-fffff") + "_" + upload.FileName;
                    var filepath = Server.MapPath("~/UserUploads");
                    var folderpath = Path.Combine(filepath, filename);
                    upload.SaveAs(folderpath);
                    informationRequest.attachment = filename;
                }
                db.Requests.Add(informationRequest);
                db.SaveChanges();
                return RedirectToAction("ConfirmationPage", "Home", new { id = informationRequest.Request_number });
            }

            return View(information);
        }

        // GET: RequestForinformation/Edit/5
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

        // POST: RequestForinformation/Edit/5
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

        // GET: RequestForinformation/Delete/5
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

        // POST: RequestForinformation/Delete/5
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
