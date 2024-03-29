﻿using System;
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
        public ActionResult Index(requestColumnName? sortColumn, bool? sortDirection)
        {
            ViewBag.sortColumn = sortColumn;
            ViewBag.sortDirection = sortDirection;
            var requestType = RequestType.installation.ToString();
            var cutOffDate = DateTime.Now.AddMonths(-1);
            IQueryable<Request> Requests = db.Requests
                .Where(r => r.Type == requestType).OrderByDescending(r => r.Request_number).Take(20)
                .sortByColumn(sortColumn, sortDirection);
            return PartialView(Requests);
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
            if (isValid)
            {

                var installationRequest = new Request
                {
                    Request_number = installation.Request_number,
                    first_name = installation.first_name,
                    last_name = installation.last_name,
                    company = installation.company,
                    contact = installation.contact,
                    description = installation.description,
                    location = installation.location,
                    Type = RequestType.installation.ToString()
                };
                if (upload != null && upload.ContentLength > 0)
                {
                    var filename = DateTime.Now.ToString("yyyyMMdd-HHmmss-fffff") + "_" + upload.FileName;
                    var filepath = Server.MapPath("~/UserUploads");
                    var folderpath = Path.Combine(filepath, filename);
                    upload.SaveAs(folderpath);
                    installationRequest.attachment = filename;
                }
                //need to redirect to page and not yellow screen of death
                else if(upload == null || upload.ContentLength == 0)
                {
                    isValid = false;
                    ModelState.AddModelError("CustomError", "Please attach an installation package");

                    return View(installation);
                }
                    db.Requests.Add(installationRequest);
                    db.SaveChanges(); 
                    return RedirectToAction("ConfirmationPage","Home",new {id = installationRequest.Request_number });
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
