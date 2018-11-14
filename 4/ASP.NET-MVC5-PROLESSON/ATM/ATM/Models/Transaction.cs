using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ATM.Models
{
    public class Transaction
    {
        public int Id { get; set; }

        // -----------------------------------------------------------------------------

        [Required]
        [DataType(DataType.Currency)]
        [Display(Name = "Amount")]
        public decimal AmountDecimal { get; set; }

        // -----------------------------------------------------------------------------

        [Display(Name = "Description")]
        public string Description { get; set; }

        // -----------------------------------------------------------------------------

        [Required]
        public int CheckingAccountId { get; set; }

        // -----------------------------------------------------------------------------

        
        [Display(Name = "Destination Account Number")]
        public string TransferDestinationCheckingAccountNumber { get; set; }

        // -----------------------------------------------------------------------------

        public virtual CheckingAccount CheckingAccount { get; set; }
    }
}