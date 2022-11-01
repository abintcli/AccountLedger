using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using AccountLedger.Services;
using AccountLedger.Test.Helpers;
using AccountLedger.Web.Models;

namespace AccountLedger.Test
{
    public class AccountServiceTest
    {
        private readonly HttpClient _httpClient;
        //private readonly IAccountService _httpClient;
        public AccountServiceTest()
        {
            _httpClient = new HttpClient() { BaseAddress = new Uri("https://localhost:7170/") };
            //_httpClient = new AccountService(new HttpClient() { BaseAddress = new Uri("https://localhost:7170/") });
        }

        //create account
        [Fact]
        public async Task GivenARequest1_WhenCallingPostAccount_ThenTheAPIReturnsExpectedResponseAndAddsAccount()
        {
            // Arrange.
            var expectedStatusCode = System.Net.HttpStatusCode.Created;
            var expectedContent = new AccountModel { Id = 1,Name = "test account 1" };
            var stopwatch = Stopwatch.StartNew();
            // Act.
            var response = await _httpClient.PostAsync("/account", TestHelpers.GetJsonStringContent(expectedContent));
            // Assert.
            await TestHelpers.AssertResponseWithContentAsync(stopwatch, response, expectedStatusCode, expectedContent);
        }

        //this will fail once there are multiple accounts in the database
        [Fact]
        public async Task GivenARequest2_WhenCallingGetAccounts_ThenTheAPIReturnsExpectedResponse()
        {
            // Arrange.
            var expectedStatusCode = System.Net.HttpStatusCode.OK;
            var expectedContent = new[]
            {
                new AccountModel { Id = 1, Name = "test account 1" }
            };
            var stopwatch = Stopwatch.StartNew();
            // Act.
            var response = await _httpClient.GetAsync("/accounts");
            // Assert.
            await TestHelpers.AssertResponseWithContentAsync(stopwatch, response, expectedStatusCode, expectedContent);
        }

        [Fact]
        public async Task GivenARequest3_WhenCallingPutAccounts_ThenTheAPIReturnsExpectedResponseAndUpdatesAccount()
        {
            // Arrange.
            var expectedStatusCode = System.Net.HttpStatusCode.NoContent;
            var updatedBook = new AccountModel { Id = 1, Name = "test account 1 updated" };
            var stopwatch = Stopwatch.StartNew();
            // Act.
            var response = await _httpClient.PutAsync($"/account/{updatedBook.Id}", TestHelpers.GetJsonStringContent(updatedBook));
            // Assert.
            TestHelpers.AssertCommonResponseParts(stopwatch, response, expectedStatusCode);
        }
        [Fact]
        public async Task GivenARequest4_WhenCallingDeleteAccount_ThenTheAPIReturnsExpectedResponseAndDeletesAccount()
        {
            // Arrange.
            var expectedStatusCode = System.Net.HttpStatusCode.OK;
            var accountIdToDelete = 1;
            var stopwatch = Stopwatch.StartNew();
            // Act.
            var response = await _httpClient.DeleteAsync($"/account/{accountIdToDelete}");
            // Assert.
            TestHelpers.AssertCommonResponseParts(stopwatch, response, expectedStatusCode);
        }


    }
}

