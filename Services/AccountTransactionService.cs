using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Transactions;
using AccountLedger.Models;

namespace AccountLedger.Services
{
    public class AccountTransactionService
    {
        static List<AccountTransaction> Transactions { get; }
        static int nextId = 3;

        static AccountTransactionService()
        {
            Transactions = new List<AccountTransaction>{
                new AccountTransaction{Id = 1, Reference= "Salary", Date= new DateTime(2022, 10,27,9,30,30), Amount = 50000, Type= TransactionType.Credit},
                new AccountTransaction{Id = 2, Reference= "Rent", Date= new DateTime(2022, 10,27,10,45,32), Amount = 6500, Type=TransactionType.Debit}
            };
        }

        public static List<AccountTransaction> GetAll() => Transactions;

        public static AccountTransaction? Get(int id) => Transactions.FirstOrDefault(t => t.Id == id);

        public static void Add(AccountTransaction transaction)
        {
            transaction.Id = nextId++;
            Transactions.Add(transaction);
        }

        public static void Delete(int id)
        {
            var transaction = Get(id);
            if (transaction is null)
                return;

            Transactions.Remove(transaction);
        }

        public static void Update(AccountTransaction transaction)
        {
            var i = Transactions.FindIndex(t => t.Id == transaction.Id);
            if (i == -1)
                return;

            Transactions[i] = transaction;
        }
    }
}