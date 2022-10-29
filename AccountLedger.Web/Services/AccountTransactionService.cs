using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Transactions;
using AccountLedger.Models;
using AccountLedger.Web.Helpers;
using AccountLedger.Web.Services;

namespace AccountLedger.Services
{
    // public class AccountTransactionService
    // {
    //     static List<AccountTransaction> Transactions { get; }
    //     static int nextId = 3;

    //     static AccountTransactionService()
    //     {
    //         Transactions = new List<AccountTransaction>{
    //             new AccountTransaction{Id = 1, Reference= "Salary", Date= new DateTime(2022, 10,27,9,30,30), Amount = 50000, Type= TransactionType.Credit},
    //             new AccountTransaction{Id = 2, Reference= "Rent", Date= new DateTime(2022, 10,27,10,45,32), Amount = 6500, Type=TransactionType.Debit}
    //         };
    //     }

    //     public static List<AccountTransaction> GetAll() => Transactions;

    //     public static AccountTransaction? Get(int id) => Transactions.FirstOrDefault(t => t.Id == id);

    //     public static void Add(AccountTransaction transaction)
    //     {
    //         transaction.Id = nextId++;
    //         Transactions.Add(transaction);
    //     }

    //     public static void Delete(int id)
    //     {
    //         var transaction = Get(id);
    //         if (transaction is null)
    //             return;

    //         Transactions.Remove(transaction);
    //     }

    //     public static void Update(AccountTransaction transaction)
    //     {
    //         var i = Transactions.FindIndex(t => t.Id == transaction.Id);
    //         if (i == -1)
    //             return;

    //         Transactions[i] = transaction;
    //     }
    // }
    public class AccountTransactionService : IAccountTransactionService
    {
        private readonly HttpClient _client;
        // public const string BasePath ="api/transactions";

        public AccountTransactionService(HttpClient client)
        {
            _client = client;
        }

        public async Task<List<AccountTransaction>> GetAll()
        {
            var response = await _client.GetAsync("/transactions");

            // return await response.ReadContentAsync<List<AccountTransaction>>();
            return await HttpClientExtensions.ReadContentAsync<List<AccountTransaction>>(response);
        }

        public async Task<AccountTransaction?> Get(int id)
        {
            var response = await _client.GetAsync($"/transaction/{id}");

            return await HttpClientExtensions.ReadContentAsync<AccountTransaction?>(response);
        }

        public async Task<AccountTransaction> Add(AccountTransaction transaction)
        {
            //this can be cleaned up alot
            var json = JsonSerializer.Serialize(transaction);
            var data = new StringContent(json, Encoding.UTF8, "application/json");
            var response = await _client.PostAsync($"/transaction", data);

            return await HttpClientExtensions.ReadContentAsync<AccountTransaction>(response);
        }

        public async Task<bool> Delete(int id)
        {
            var response = await _client.DeleteAsync($"/transaction/{id}");

            await HttpClientExtensions.ReadContentAsync<object>(response);

            return true;
        }

        public async Task<bool> Update(AccountTransaction transaction, int id)
        {
            //this can be cleaned up alot
            var json = JsonSerializer.Serialize(transaction);
            var data = new StringContent(json);
            var response = await _client.PutAsync($"/transaction/{id}", data);

            await HttpClientExtensions.ReadContentAsync<object>(response);
            
            // an exception would be thrown if the update failed. need to refactor to include error messages
            return true;
        }
    }
}