using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ATM.Models;
using Microsoft.AspNet.Identity;

namespace ATM.Controllers
{
    [Authorize]
    public class PaymentController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        public ActionResult Create()
        {
            
            return View();
        }

        [HttpPost]
        public ActionResult Create(Payment payment)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }
            var userId = User.Identity.GetUserId();
            var checkingAccount = db.CheckingAccounts.First(c => c.ApplicationUserId == userId);
            payment.CheckingAccount = checkingAccount;
            db.Payments.Add(payment);
            db.SaveChanges();

            return RedirectToAction("Index", "Home");
        }
       
    }
}
