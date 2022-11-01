﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace AccountLedger.Test.Helpers
{
    public static class TestHelpers
    {
        private const string _jsonMediaType = "application/json";
        private const int _expectedMaxElapsedMilliseconds = 1000;
        private static readonly JsonSerializerOptions _jsonSerializerOptions = new() { PropertyNameCaseInsensitive = true };

        public static async Task AssertResponseWithContentAsync<T>(Stopwatch stopwatch,
            HttpResponseMessage response, System.Net.HttpStatusCode expectedStatusCode,
            T expectedContent)
        {
            AssertCommonResponseParts(stopwatch, response, expectedStatusCode);
            Assert.Equal(_jsonMediaType, response.Content.Headers.ContentType?.MediaType);
            //var result = await JsonSerializer.DeserializeAsync<T?>(await response.Content.ReadAsStreamAsync(), _jsonSerializerOptions);

            var actualOutput = JsonSerializer.Serialize(
                                await JsonSerializer.DeserializeAsync<T?>(
                                await response.Content.ReadAsStreamAsync(), _jsonSerializerOptions));
            var expectedOutput = JsonSerializer.Serialize(expectedContent);
            //Assert.Equal(expectedContent, await JsonSerializer.DeserializeAsync<T?>(await response.Content.ReadAsStreamAsync(), _jsonSerializerOptions));
            Assert.Equal(expectedOutput, actualOutput);
        }
        public static void AssertCommonResponseParts(Stopwatch stopwatch,
            HttpResponseMessage response, System.Net.HttpStatusCode expectedStatusCode)
        {
            Assert.Equal(expectedStatusCode, response.StatusCode);
            Assert.True(stopwatch.ElapsedMilliseconds < _expectedMaxElapsedMilliseconds);
        }
        public static StringContent GetJsonStringContent<T>(T model)
            => new(JsonSerializer.Serialize(model), Encoding.UTF8, _jsonMediaType);
    }
}
