using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using AccountLedger.Services;
using AccountLedger.Test.Attributes;
using AccountLedger.Test.Helpers;
using AccountLedger.Web.Models;

namespace AccountLedger.Test
{
    [TestCaseOrderer("AccountLedger.Test.Orderers.PriorityOrderer", "XUnit.Project")]
    public class AccountServiceTest
    {
        private readonly HttpClient _httpClient;
        private readonly IAccountService _account;
        public AccountServiceTest()
        {
            _httpClient = new HttpClient() { BaseAddress = new Uri("https://localhost:7170/") };
            _account = new AccountService(new HttpClient() { BaseAddress = new Uri("https://localhost:7170/") });
        }

        //create account
        [Fact, TestPriority(1)]
        public async Task GivenARequest1_WhenCallingPostAccount_ThenTheAPIReturnsExpectedResponseAndAddsAccount()
        {
            // preconditions - we need to see what the account id will be to test that we got the right result
            var accounts = await _account.GetAll();
            var accountId = accounts.Max(a => a.Id) + 1;

            // Arrange.
            var expectedStatusCode = System.Net.HttpStatusCode.Created;
            var expectedContent = new AccountModel { Id = accountId, Name = "test account" };
            var stopwatch = Stopwatch.StartNew();
            // Act.
            var response = await _httpClient.PostAsync("/account", TestHelpers.GetJsonStringContent(expectedContent));
            // Assert.
            await TestHelpers.AssertResponseWithContentAsync(stopwatch, response, expectedStatusCode, expectedContent);

            // Cleanup - we need to remove anythin we created
            await _account.Delete(accountId);
        }

        //checks that we get all accounts
        [Fact, TestPriority(2)]
        public async Task GivenARequest2_WhenCallingGetAccounts_ThenTheAPIReturnsExpectedResponse()
        {
            // preconditions - we need to add a new account to see if it added to the list;
            var accounts = await _account.GetAll();
            var accountId = accounts.Max(a => a.Id) + 1;
            var newContent = new AccountModel { Id = accountId, Name = "test account" };
            await _account.Add(newContent);

            // Arrange.
            var expectedStatusCode = System.Net.HttpStatusCode.OK;
            var expectedContent = accounts;
            expectedContent.Add(newContent);
            var stopwatch = Stopwatch.StartNew();
            // Act.
            var response = await _httpClient.GetAsync("/accounts");
            // Assert.
            await TestHelpers.AssertResponseWithContentAsync(stopwatch, response, expectedStatusCode, expectedContent);

            // Cleanup - we need to remove anythin we created
            await _account.Delete(accountId);
        }

        //this will fail once there are multiple accounts in the database
        [Fact, TestPriority(3)]
        public async Task GivenARequest3_WhenCallingGetAccount_ThenTheAPIReturnsExpectedResponse()
        {
            // preconditions - we need to add a new account to retrieve;
            var accounts = await _account.GetAll();
            var accountId = accounts.Max(a => a.Id)+1;
            var newContent = new AccountModel { Id = accountId, Name = "test account" };
            await _account.Add(newContent);

            // Arrange.
            var expectedStatusCode = System.Net.HttpStatusCode.OK;
            var expectedContent = newContent;            
            var stopwatch = Stopwatch.StartNew();
            // Act.
            var response = await _httpClient.GetAsync($"/account/{expectedContent.Id}");
            // Assert.
            await TestHelpers.AssertResponseWithContentAsync(stopwatch, response, expectedStatusCode, expectedContent);

            // Cleanup - we need to remove anythin we created
            await _account.Delete(accountId);
        }

        [Fact, TestPriority(4)]
        public async Task GivenARequest4_WhenCallingPutAccounts_ThenTheAPIReturnsExpectedResponseAndUpdatesAccount()
        {
            // preconditions - we need to add a new account to update;
            var accounts = await _account.GetAll();
            var accountId = accounts.Max(a => a.Id)+1;
            var newContent = new AccountModel { Id = accountId, Name = "test account" };
            await _account.Add(newContent);

            // Arrange.
            var expectedStatusCode = System.Net.HttpStatusCode.NoContent;
            var updatedAccount = new AccountModel { Id = accountId, Name = "test account updated" };
            var stopwatch = Stopwatch.StartNew();
            // Act.
            var response = await _httpClient.PutAsync($"/account/{updatedAccount.Id}", TestHelpers.GetJsonStringContent(updatedAccount));
            // Assert.
            TestHelpers.AssertCommonResponseParts(stopwatch, response, expectedStatusCode);

            // Cleanup - we need to remove anythin we created
            await _account.Delete(accountId);
        }

        [Fact, TestPriority(5)]
        public async Task GivenARequest5_WhenCallingDeleteAccount_ThenTheAPIReturnsExpectedResponseAndDeletesAccount()
        {
            // preconditions - we need to add a new account to delete;
            var accounts = await _account.GetAll();
            var accountId = accounts.Max(a => a.Id)+1;
            var newContent = new AccountModel { Id = accountId, Name = "test account" };
            await _account.Add(newContent);

            // Arrange.
            var expectedStatusCode = System.Net.HttpStatusCode.OK;
            var accountIdToDelete = accountId;
            var stopwatch = Stopwatch.StartNew();
            // Act.
            var response = await _httpClient.DeleteAsync($"/account/{accountIdToDelete}");
            // Assert.
            TestHelpers.AssertCommonResponseParts(stopwatch, response, expectedStatusCode);

        }


    }
}

