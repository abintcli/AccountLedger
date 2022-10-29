using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AccountLedger.API.Models
{
    public class Account
    {
        public int Id { get; set; }
        public List<AccountTransaction>? Transactions { get; set; }
        public string Name { get; set; }
    }
}