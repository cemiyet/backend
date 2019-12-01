using System;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Xunit;

namespace Cemiyet.Api.Tests.Extensions
{
    public static class HttpClientExtensions
    {
        public static async Task<HttpResponseMessage> SendRequestMessageAsync(this HttpClient client, HttpMethod method,
                                                                              string uri, object content)
        {
            return await client.SendAsync(new HttpRequestMessage
            {
                Method = method,
                RequestUri = new Uri(client.BaseAddress + uri),
                Content = new StringContent(JsonConvert.SerializeObject(content), Encoding.UTF8, "application/json")
            });
        }

        public static async Task<HttpResponseMessage> AssertedGetAsync(this HttpClient client, string uri, HttpStatusCode code)
        {
            var response = await client.GetAsync(uri);
            Assert.Equal(code, response.StatusCode);
            return response;
        }
    }
}
