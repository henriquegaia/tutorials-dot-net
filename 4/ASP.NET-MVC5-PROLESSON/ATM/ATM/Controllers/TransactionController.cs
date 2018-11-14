using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Web;
using System.Web.Mvc;
using ATM.Models;
using ATM.Services;
using Microsoft.AspNet.Identity;

namespace ATM.Controllers
{
    [Authorize]
    public class TransactionController : Controller
    {
        // doesn't allow integration tests without connecting to db
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: /transaction/deposit
        public ActionResult Deposit(int checkingAccountId)
        {
            return View();
        }

        [HttpPost]
        public ActionResult Deposit(Transaction deposit)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }
            db.Transactions.Add(deposit);
            db.SaveChanges();
            var service = new CheckingAccountService(db);
            service.UpdateBalance(deposit.CheckingAccountId);
            return RedirectToAction("Index", "Home");
        }

        // --------------------------------------------------------------------------------------


        public ActionResult Withdraw()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Withdraw(Transaction withdraw)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }
            var service = new CheckingAccountService(db);
            if (withdraw.AmountDecimal > service.GetBalance(withdraw.CheckingAccountId))
            {
                return View();
            }
            withdraw.AmountDecimal *= (-1);
            db.Transactions.Add(withdraw);
            db.SaveChanges();

            service.UpdateBalance(withdraw.CheckingAccountId);
            return RedirectToAction("Index", "Home");
        }

        // --------------------------------------------------------------------------------------

        public ActionResult QuickCash100()
        {
            return View();
        }

        [HttpPost]
        public ActionResult QuickCash100(Transaction withdraw)
        {
            withdraw.AmountDecimal = 100;
            return Withdraw(withdraw);
        }

        // --------------------------------------------------------------------------------------


        public ActionResult Transfer()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Transfer(Transaction transfer)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }
            var service = new CheckingAccountService(db);
            string checkingAccountNumberDestiny =
                service.GetCheckingAccountNumber(transfer.TransferDestinationCheckingAccountNumber,
                    transfer.CheckingAccountId);
            if ("0".Equals(checkingAccountNumberDestiny))
            {
                return View();
            }
            db.Transactions.Add(transfer);
            db.SaveChanges();

            //Debit
            // TODO: debit on transfer
            //Credit
            // TODO: credit on transfer
            return RedirectToAction("Index", "Home");
        }

        // --------------------------------------------------------------------------------------

        public ActionResult List()
        {
            return View(db.Transactions.ToList());
        }
    }
}