using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ATM.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;


namespace ATM.Controllers
{
    public class HomeController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        [Authorize]
        public ActionResult Index()
        {
            var userId = User.Identity.GetUserId();
            var checkingAccountId = db.CheckingAccounts.First(c => c.ApplicationUserId == userId).Id;
            ViewBag.CheckingAccountId = checkingAccountId;
            // Optional: show pin (01:59:50 - 02:01:40)
            //var manager = HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
            //var user = manager.FindById(userId);
            //ViewBag.Pin = user.Pin;
            // end optional
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Foo()
        {
            return View("About");
        }

        public ActionResult Contact()
        {
            ViewBag.TheMessage = "Having trouble? Send us a message.";

            return View();
        }

        [HttpPost]
        public ActionResult Contact(string message)
        {
            ViewBag.TheMessage = "Thanks we got your message: '" + message + "'";

            return View();
        }

        public ActionResult Serial(string letterCase)
        {
            if (letterCase == "lower")
            {
                return Content(letterCase.ToLower());
            }
            return Content(letterCase.ToUpper());
        }
    }
}