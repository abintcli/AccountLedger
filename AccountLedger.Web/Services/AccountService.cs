using AccountLedger.Web.Helpers;
using AccountLedger.Web.Models;
using System.Text;
using System.Text.Json;

namespace AccountLedger.Services
{
    public class AccountService :IAccountService
    {
        private readonly HttpClient _client;
        // public const string BasePath ="api/transactions";

        public AccountService(HttpClient client)
        {
            _client = client;
        }

        public async Task<List<AccountModel>> GetAll()
        {
            var response = await _client.GetAsync("/accounts");

            return await HttpClientExtensions.ReadContentAsync<List<AccountModel>>(response);
        }

        public async Task<AccountModel?> Get(int id)
        {
            var response = await _client.GetAsync($"/account/{id}");

            return await HttpClientExtensions.ReadContentAsync<AccountModel?>(response);
        }

        public async Task<AccountModel> Add(AccountModel transaction)
        {
            //this can be cleaned up alot
            var json = JsonSerializer.Serialize(transaction);
            var data = new StringContent(json, Encoding.UTF8, "application/json");
            var response = await _client.PostAsync($"/account", data);

            return await HttpClientExtensions.ReadContentAsync<AccountModel>(response);
        }

        public async Task<bool> Delete(int id)
        {
            var response = await _client.DeleteAsync($"/account/{id}");

            await HttpClientExtensions.ReadContentAsync<object>(response);

            return true;
        }

        public async Task<bool> Update(AccountModel transaction, int id)
        {
            //this can be cleaned up alot
            var json = JsonSerializer.Serialize(transaction);
            var data = new StringContent(json, Encoding.UTF8, "application/json");
            var response = await _client.PutAsync($"/account/{id}", data);

            await HttpClientExtensions.ReadContentAsync<object>(response);

            // an exception would be thrown if the update failed. need to refactor to include error messages
            return true;
        }
    }
}
