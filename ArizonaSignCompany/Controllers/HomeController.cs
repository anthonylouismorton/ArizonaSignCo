﻿using ArizonaSignCompany.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ArizonaSignCompany.Controllers
{
    //[Authorize]
    // Will make all pages require login if this is before the class controller
    public class HomeController : Controller
    {
        private ArizonaSignCompanyEntities db = new ArizonaSignCompanyEntities();
        //[AllowAnonymous]
        //Will allow users to see the home page if you use the authorize attribute above the class
        public ActionResult Index()
        {


            return View();
        }


        public ActionResult About()
        {
            var userID = User.Identity.GetUserId();
            ViewBag.UserId = userID;

            var username = User.Identity.GetUserName();
            ViewBag.Username = username;
            if (Request.IsAuthenticated)
            {
                using (var userManager = HttpContext.GetOwinContext().Get<ApplicationUserManager>())
                {
                    ApplicationUser applicationUser = userManager.Users.Single(u => u.Id == userID);
                    ViewBag.Pass = applicationUser.PasswordHash;
                }
                ViewBag.UserMessage = "Welcome, registered and logged in user!";
            }
            else
            {
                ViewBag.UserMessage = "Welcome, new user! Please consider registering";
            }
            ViewBag.Message = "Your application description page.";

            return View();
        }
        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        public ActionResult Lighting()
        {
            ViewBag.Message = "Efficient-Lighting Page.";

            return View();
        }

        public ActionResult Calendar()
        {
            ViewBag.Message = "Calendar";

            return PartialView();
        }

        public ActionResult ConfirmationPage(int id)
        {
            var request = db.Requests.Find(id);
            return View(request);
        }

    }
}