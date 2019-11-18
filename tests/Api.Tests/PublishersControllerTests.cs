using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Cemiyet.Application.Publishers.Commands.DeleteMany;
using Cemiyet.Core.Entities;
using Newtonsoft.Json;
using Xunit;

namespace Cemiyet.Api.Tests
{
    public class PublishersControllerTests : IntegrationTest
    {
        [Fact]
        public async Task Add_WithoutCorrectData_ShouldReturn_BadRequest()
        {
            var response = await _httpClient.PostAsJsonAsync("publishers/", default(Publisher));
            var response2 = await _httpClient.PostAsJsonAsync("publishers/", new Publisher
            {
                Name = ""
            });

            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
            Assert.Equal(HttpStatusCode.BadRequest, response2.StatusCode);
        }

        [Fact]
        public async Task Add_WithCorrectData_ShouldReturn_OK()
        {
            var response = await _httpClient.PostAsJsonAsync("publishers/", new Publisher
            {
                Name = "Yayıncılık",
                Description = "Veli"
            });

            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }

        [Fact]
        public async Task List_WithoutCorrectPaging_ShouldReturn_BadRequest()
        {
            var response = await _httpClient.GetAsync("publishers?page=-1&pageSize=-5");
            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
        }

        [Fact]
        public async Task List_WithoutPaging_ShouldReturn_DefaultPagedResult()
        {
            var response = await _httpClient.GetAsync("publishers");
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);

            var responseData = await response.Content.ReadAsAsync<List<Publisher>>();
            Assert.NotEmpty(responseData);
        }

        [Fact]
        public async Task Details_WithoutCorrectId_ShouldReturn_BadRequest()
        {
            var response = await _httpClient.GetAsync($"publishers/{Guid.NewGuid()}");
            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
        }

        [Fact]
        public async Task Details_WithCorrectId_ShouldReturn_PublisherObject()
        {
            var publishersResponse = await _httpClient.GetAsync("publishers");
            var publishers = await publishersResponse.Content.ReadAsAsync<List<Publisher>>();

            var response = await _httpClient.GetAsync($"publishers/{publishers.First().Id}");
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);

            var responseData = await response.Content.ReadAsAsync<Publisher>();
            Assert.NotNull(responseData);
        }

        [Fact]
        public async Task UpdatePartially_WithoutCorrectData_ShouldReturn_BadRequest()
        {
            var publishersResponse = await _httpClient.GetAsync("publishers");
            var publishers = await publishersResponse.Content.ReadAsAsync<List<Publisher>>();

            var request = new HttpRequestMessage
            {
                Method = HttpMethod.Patch,
                RequestUri = new Uri(_httpClient.BaseAddress + $"publishers/{publishers.First().Id}"),
                Content = new StringContent(JsonConvert.SerializeObject(new { }), Encoding.UTF8, "application/json")
            };

            var response = await _httpClient.SendAsync(request);
            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
        }

        [Fact]
        public async Task UpdatePartially_WithCorrectData_ShouldReturn_OK()
        {
            var publishersResponse = await _httpClient.GetAsync("publishers");
            var publishers = await publishersResponse.Content.ReadAsAsync<List<Publisher>>();

            var request = new HttpRequestMessage
            {
                Method = HttpMethod.Patch,
                RequestUri = new Uri(_httpClient.BaseAddress + $"publishers/{publishers.First().Id}"),
                Content = new StringContent(JsonConvert.SerializeObject(new { Name = "YAYINEVİ BİR" }), Encoding.UTF8,
                                            "application/json")
            };

            var response = await _httpClient.SendAsync(request);
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }

        [Fact]
        public async Task Update_WithoutCorrectId_ShouldReturn_BadRequest()
        {
            var response = await _httpClient.PutAsJsonAsync($"publishers/{Guid.NewGuid()}", default(Publisher));
            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
        }

        [Fact]
        public async Task Update_WithoutCorrectData_ShouldReturn_BadRequest()
        {
            var publishersResponse = await _httpClient.GetAsync("publishers");
            var publishers = await publishersResponse.Content.ReadAsAsync<List<Publisher>>();

            var response = await _httpClient.PutAsJsonAsync($"publishers/{publishers.First().Id}", default(Publisher));
            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);

            response = await _httpClient.PutAsJsonAsync($"publishers/{publishers.First().Id}", new Publisher
            {
                Name = "5",
                Description = null
            });

            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
        }

        [Fact]
        public async Task Update_WithCorrectData_ShouldReturn_OK()
        {
            var publishersResponse = await _httpClient.GetAsync("publishers");
            var publishers = await publishersResponse.Content.ReadAsAsync<List<Publisher>>();

            var response = await _httpClient.PutAsJsonAsync($"publishers/{publishers.Last().Id}", new
            {
                Name = "YKY",
                Description = "ABCDEFGH"
            });

            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }

        [Fact]
        public async Task DeleteOne_WithoutCorrectId_ShouldReturn_BadRequest()
        {
            var response = await _httpClient.DeleteAsync($"publishers/{Guid.NewGuid()}");
            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
        }

        [Fact]
        public async Task DeleteOne_WithCorrectId_ShouldReturn_OK()
        {
            var publishersResponse = await _httpClient.GetAsync("publishers");
            var publishers = await publishersResponse.Content.ReadAsAsync<List<Publisher>>();

            var response = await _httpClient.DeleteAsync($"publishers/{publishers.Last().Id}");
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }

        [Fact]
        public async Task DeleteMany_WithoutCorrectIds_ShouldReturn_BadRequest()
        {
            string[] ids = { Guid.NewGuid().ToString(), Guid.NewGuid().ToString() };

            var request = new HttpRequestMessage
            {
                Method = HttpMethod.Delete,
                RequestUri = new Uri(_httpClient.BaseAddress + "publishers"),
                Content = new StringContent(JsonConvert.SerializeObject(ids), Encoding.UTF8, "application/json")
            };

            var response = await _httpClient.SendAsync(request);
            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
        }

        [Fact]
        public async Task DeleteMany_WithCorrectIds_ShouldReturn_OK()
        {
            var publishersResponse = await _httpClient.GetAsync("publishers");
            var publishers = await publishersResponse.Content.ReadAsAsync<List<Publisher>>();

            var dmc = new DeleteManyCommand { Ids = publishers.TakeLast(2).Select(g => g.Id).ToArray() };

            var request = new HttpRequestMessage
            {
                Method = HttpMethod.Delete,
                RequestUri = new Uri(_httpClient.BaseAddress + "publishers"),
                Content = new StringContent(JsonConvert.SerializeObject(dmc), Encoding.UTF8, "application/json")
            };

            var response = await _httpClient.SendAsync(request);
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }
    }
}
