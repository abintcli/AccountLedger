using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AccountLedger.Models
{
    public class AccountTransaction
    {
        public int Id { get; set; }
        // public int AccountId { get; set;}

        [Required]
        public DateTime Date
        {
            get
            {
                return this.dateCreated ?? DateTime.Now;
            }
            set
            {
                this.dateCreated = value;
            }
        }
        private DateTime? dateCreated = null;

        [Required]
        public TransactionType Type { get; set; }

        [Required]
        public string? Reference { get; set; }

        [Required]
        //may be able to remove this - do i want to limit the amount?
        //[Range(0.01, 100000.00)]
        public decimal Amount { get; set; }

    }

    public enum TransactionType { Credit, Debit }
}