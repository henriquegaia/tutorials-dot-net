using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ATM.Models
{
    public class Payment
    {

        public int Id { get; set; }

        // -----------------------------------------------------------------------------

        [Required]
        public int Entity { get; set; }

        // -----------------------------------------------------------------------------

        [Required]
        public int Reference { get; set; }

        // -----------------------------------------------------------------------------

        [Required]
        public decimal AmountDecimal { get; set; }

        // -----------------------------------------------------------------------------

        public virtual CheckingAccount CheckingAccount { get; set; }
    }
}