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
    public class TransactionService : ITransactionService
    {
        private readonly HttpClient _client;
        // public const string BasePath ="api/transactions";

        public TransactionService(HttpClient client)
        {
            _client = client;
        }

        public async Task<List<AccountTransaction>> GetAll(int accountId)
        {
            //need to have a look again
            var response = await _client.GetAsync($"/account/{accountId}/transactions");

            // return await response.ReadContentAsync<List<AccountTransaction>>();
            return await HttpClientExtensions.ReadContentAsync<List<AccountTransaction>>(response);
        }

        public async Task<List<AccountTransaction>> GetAllFilter(int accountId, TransactionType type)
        {
            //need to have a look again
            var response = await _client.GetAsync($"/account/{accountId}/transactions/{type}");

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
            var data = new StringContent(json, Encoding.UTF8, "application/json");
            var response = await _client.PutAsync($"/transaction/{id}", data);

            await HttpClientExtensions.ReadContentAsync<object>(response);
            
            // an exception would be thrown if the update failed. need to refactor to include error messages
            return true;
        }
    }
}