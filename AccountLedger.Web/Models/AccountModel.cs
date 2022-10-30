using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using AccountLedger.Models;

namespace AccountLedger.Web.Models
{
    public class AccountModel
    {
        public int Id { get; set; }
        public List<AccountTransaction>? Transactions { get; set; }
        
        [Required]
        public string Name { get; set; }

    }
}