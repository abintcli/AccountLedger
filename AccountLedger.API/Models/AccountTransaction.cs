using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace AccountLedger.API.Models
{
    public class AccountTransaction
    {
        public int Id { get; set; }
        // public int AccountId { get; set; }
        public DateTime Date { get; set; }
        public TransactionType Type { get; set; }
        public string Reference { get; set; }
        public decimal Amount { get; set; }
        public int AccountId{get;set;}
    }

    public enum TransactionType { Credit, Debit }

    class TransactionDb : DbContext
    {
        public TransactionDb(DbContextOptions options) : base(options) { }
        public DbSet<AccountTransaction> Transactions { get; set; } = null!;
        public DbSet<Account> Accounts { get; set; } = null!;
    }
}