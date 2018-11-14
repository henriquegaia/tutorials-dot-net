using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Configuration;
using System.Linq;
using System.Web;

namespace ATM.Models
{
    public class CheckingAccount
    {
        public int Id { get; set; }

        /*
         * ----------------------------------------
         * account #
         * ----------------------------------------
         */
        [Required]
        [Column(TypeName="varchar")]
        [RegularExpression(@"\d{6,10}", ErrorMessage = "Must be between 6 and 10 digits")]
        [Display(Name = "Account #")]
        public string AccountNumber { get; set; }

        /*
         * ----------------------------------------
         * fname
         * ----------------------------------------
         */
        [Required]
        [Display(Name = "First Name")]
        public string FirstName { get; set; }

        /*
         * ----------------------------------------
         * lname
         * ----------------------------------------
         */
        [Required]
        [Display(Name = "Last Name")]
        public string LastName { get; set; }

        /*
         * ----------------------------------------
         * name
         * ----------------------------------------
         */
        [Display(Name = "Name")]
        public string Name
        {
            get
            {
                return String.Format("{0} {1}", this.FirstName, this.LastName);
            }
        }

        /*
         * ----------------------------------------
         * balance
         * ----------------------------------------
         */
        [Required]
        [DataType(DataType.Currency)]
        public decimal Balance { get; set; }

        /*
         * ----------------------------------------
         * reference to user that holds the account
         * ----------------------------------------
         */
        public virtual ApplicationUser User { get; set; }

        [Required]
        public string ApplicationUserId { get; set; }

        public virtual ICollection<Transaction> Transactions { get; set; }

    }
}