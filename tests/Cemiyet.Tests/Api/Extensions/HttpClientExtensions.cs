using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Xunit;

namespace Cemiyet.Tests.Api.Extensions
{
    public static class HttpClientExtensions
    {
        public static async Task<HttpResponseMessage> AssertedGetAsync(this HttpClient client, string uri,
                                                                       HttpStatusCode code)
        {
            var response = await client.GetAsync(uri);
            Assert.Equal(code, response.StatusCode);
            return response;
        }

        public static async Task<List<T>> AssertedGetEntityListFromUri<T>(this HttpClient client, string uri)
        {
            var response = await client.AssertedGetAsync(uri, HttpStatusCode.OK);
            var collection = await response.Content.ReadAsAsync<List<T>>();
            Assert.NotEmpty(collection);
            return collection;
        }

        public static async Task<HttpResponseMessage> AssertedSendRequestMessageAsync(this HttpClient client,
                                                                                      HttpMethod method,
                                                                                      string uri,
                                                                                      object content,
                                                                                      HttpStatusCode code)
        {
            var response = await client.SendAsync(new HttpRequestMessage
            {
                Method = method,
                RequestUri = new Uri(client.BaseAddress + uri),
                Content = new StringContent(JsonConvert.SerializeObject(content), Encoding.UTF8, "application/json")
            });
            Assert.Equal(code, response.StatusCode);
            return response;
        }
    }
}
