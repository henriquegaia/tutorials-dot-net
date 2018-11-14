using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ATM.Models;

namespace ATM.Services
{
    public class CheckingAccountService
    {

        private ApplicationDbContext db;

        public CheckingAccountService(ApplicationDbContext dbContext)
        {
            db = dbContext;
        }

        public void CreateCheckingAccount(string firstName, string lastName, string userId, decimal initialBalance)
        {
            var accountNumber = (123456 + db.CheckingAccounts.Count().ToString()).PadLeft(10, '0');
            var checkingAccount = new CheckingAccount()
            {
                FirstName = firstName,
                LastName = lastName,
                AccountNumber = accountNumber,
                Balance = initialBalance,
                ApplicationUserId = userId
            };
            db.CheckingAccounts.Add(checkingAccount);
            db.SaveChanges();
        }

        public void UpdateBalance(int checkingAccountId)
        {
            var checkingAccount = db.CheckingAccounts.First(c => c.Id == checkingAccountId);
            checkingAccount.Balance =
                db.Transactions.Where(t => t.CheckingAccountId == checkingAccountId).Sum(c => c.AmountDecimal);
            db.SaveChanges();

        }

        public decimal GetBalance(int checkingAccountId)
        {
            var checkingAccount = db.CheckingAccounts.First(c => c.Id == checkingAccountId);
            return checkingAccount.Balance;
        }

        public string GetCheckingAccountNumber(string checkingAccountNumber, int checkingAccountId)
        {
            try
            {
                // check if exists
                var checkingAccount = db.CheckingAccounts.First(c => c.AccountNumber == checkingAccountNumber);

                // check if different than user account
                if (checkingAccountId == checkingAccount.Id)
                {
                    return "0";
                }

            }
            catch (Exception e)
            {
                return "0";
            }

            return checkingAccountNumber;
        }

        public int GetCheckingAccountId(string checkingAccountNumber)
        {
            var checkingAccount = db.CheckingAccounts.First(c => c.AccountNumber == checkingAccountNumber);
            return checkingAccount.Id;
        }

    }
}