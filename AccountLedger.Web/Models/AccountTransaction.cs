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

        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:0}")]
        //[UIHint("Currency")]
        //may be able to remove this - do i want to limit the amount?
        ////[Range(0.01, 100000.00)]
        //[Required(ErrorMessage = "Amount is required")]
        ////[Range(typeof(decimal), "0.01", "100000.00", ErrorMessage = "Enter decimal value")]
        //[RegularExpression(@"^\[0-9]{1,6}\.[0-9]{2}", ErrorMessage = "Enter decimal value of format 0.00")]
        [Required]
        public decimal Amount { get; set; }

        public int AccountId{get;set;}
    }

    public enum TransactionType { Credit, Debit }
}