using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AccountLedger.API.DB
{
    public class AccountTransaction
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public TransactionType Type { get; set; }
        public string? Reference { get; set; }
        public decimal Amount { get; set; }
    }

    public enum TransactionType { Credit, Debit }

    public class TransactionDB
    {
        private static List<AccountTransaction> _transactions = new List<AccountTransaction>(){
            new AccountTransaction{Id = 1, Reference= "Salary", Date= new DateTime(2022, 10,27,9,30,30), Amount = 50000, Type= TransactionType.Credit},
            new AccountTransaction{Id = 2, Reference= "Rent", Date= new DateTime(2022, 10,27,10,45,32), Amount = 6500, Type=TransactionType.Debit}
        };

        public static List<AccountTransaction> GetTransactions()
        {
            return _transactions;
        }

        public static AccountTransaction? GetTransaction(int id)
        {
            return _transactions.SingleOrDefault(t => t.Id == id);
        }

        public static AccountTransaction CreateTransaction(AccountTransaction transaction)
        {
            _transactions.Add(transaction);
            return transaction;
        }

        public static AccountTransaction UpdateTransaction(AccountTransaction update)
        {
            _transactions = _transactions.Select(t =>
            {
                if (t.Id == update.Id)
                {
                    // check if this works!!!
                    t = update;
                }
                return t;
            }).ToList();

            return update;
        }

        public static void RemoveTransaction(int id)
        {
            _transactions = _transactions.FindAll(t => t.Id != id).ToList();
        }
    }
}