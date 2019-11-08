using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Cemiyet.Application.Dimensions.Commands.DeleteMany;
using Cemiyet.Core.Entities;
using Newtonsoft.Json;
using Xunit;

namespace Cemiyet.Api.Tests
{
    public class DimensionsControllerTests : IntegrationTest
    {
        [Fact]
        public async Task Add_WithoutCorrectData_ShouldReturn_BadRequest()
        {
            var response = await _httpClient.PostAsJsonAsync("dimensions/", default(Dimension));

            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
        }

        [Fact]
        public async Task Add_WithCorrectData_ShouldReturn_OK()
        {
            var response = await _httpClient.PostAsJsonAsync("dimensions/", new Dimension
            {
                Width = 1.5,
                Height = 2.5
            });

            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }

        [Fact]
        public async Task List_WithoutCorrectPaging_ShouldReturn_BadRequest()
        {
            var response = await _httpClient.GetAsync("dimensions?page=-1&pageSize=-5");
            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
        }

        [Fact]
        public async Task List_WithoutPaging_ShouldReturn_DefaultPagedResult()
        {
            var response = await _httpClient.GetAsync("dimensions");
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);

            var responseData = await response.Content.ReadAsAsync<List<Dimension>>();
            Assert.NotEmpty(responseData);
        }

        [Fact]
        public async Task Details_WithoutCorrectId_ShouldReturn_BadRequest()
        {
            var response = await _httpClient.GetAsync($"dimensions/{Guid.NewGuid()}");
            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
        }

        [Fact]
        public async Task Details_WithCorrectId_ShouldReturn_DimensionObject()
        {
            var dimensionsResponse = await _httpClient.GetAsync("dimensions");
            var dimensions = await dimensionsResponse.Content.ReadAsAsync<List<Dimension>>();

            var response = await _httpClient.GetAsync($"dimensions/{dimensions.First().Id}");
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);

            var responseData = await response.Content.ReadAsAsync<Dimension>();
            Assert.NotNull(responseData);
        }

        [Fact]
        public async Task Update_WithoutCorrectId_ShouldReturn_BadRequest()
        {
            var response = await _httpClient.PutAsJsonAsync($"dimensions/{Guid.NewGuid()}", default(Dimension));
            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
        }

        [Fact]
        public async Task Update_WithoutCorrectData_ShouldReturn_BadRequest()
        {
            var dimensionsResponse = await _httpClient.GetAsync("dimensions");
            var dimensions = await dimensionsResponse.Content.ReadAsAsync<List<Dimension>>();

            var response = await _httpClient.PutAsJsonAsync($"dimensions/{dimensions.First().Id}", default(Dimension));
            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);

            response = await _httpClient.PutAsJsonAsync($"dimensions/{dimensions.First().Id}", new
            {
                Width = 5.0
            });
            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
        }

        [Fact]
        public async Task Update_WithCorrectData_ShouldReturn_OK()
        {
            var dimensionsResponse = await _httpClient.GetAsync("dimensions");
            var dimensions = await dimensionsResponse.Content.ReadAsAsync<List<Dimension>>();

            var response = await _httpClient.PutAsJsonAsync($"dimensions/{dimensions.Last().Id}", new
            {
                Width = 1.2,
                Height = 1.6
            });

            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }

        [Fact]
        public async Task UpdatePartially_WithoutCorrectData_ShouldReturn_BadRequest()
        {
            var dimensionsResponse = await _httpClient.GetAsync("dimensions");
            var dimensions = await dimensionsResponse.Content.ReadAsAsync<List<Dimension>>();

            var request = new HttpRequestMessage
            {
                Method = HttpMethod.Patch,
                RequestUri = new Uri(_httpClient.BaseAddress + $"dimensions/{dimensions.First().Id}"),
                Content = new StringContent(JsonConvert.SerializeObject(new { }), Encoding.UTF8, "application/json")
            };

            var response = await _httpClient.SendAsync(request);
            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
        }

        [Fact]
        public async Task UpdatePartially_WithCorrectData_ShouldReturn_OK()
        {
            var dimensionsResponse = await _httpClient.GetAsync("dimensions");
            var dimensions = await dimensionsResponse.Content.ReadAsAsync<List<Dimension>>();

            var request = new HttpRequestMessage
            {
                Method = HttpMethod.Patch,
                RequestUri = new Uri(_httpClient.BaseAddress + $"dimensions/{dimensions.First().Id}"),
                Content = new StringContent(JsonConvert.SerializeObject(new {Width = 5.0}), Encoding.UTF8,
                                            "application/json")
            };

            var response = await _httpClient.SendAsync(request);
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }

        [Fact]
        public async Task DeleteOne_WithoutCorrectId_ShouldReturn_BadRequest()
        {
            var response = await _httpClient.DeleteAsync($"dimensions/{Guid.NewGuid()}");
            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
        }

        [Fact]
        public async Task DeleteOne_WithCorrectId_ShouldReturn_OK()
        {
            var dimensionsResponse = await _httpClient.GetAsync("dimensions");
            var dimensions = await dimensionsResponse.Content.ReadAsAsync<List<Dimension>>();

            var response = await _httpClient.DeleteAsync($"dimensions/{dimensions.Last().Id}");
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }

        [Fact]
        public async Task DeleteMany_WithoutCorrectIds_ShouldReturn_BadRequest()
        {
            string[] ids = {Guid.NewGuid().ToString(), Guid.NewGuid().ToString()};

            var request = new HttpRequestMessage
            {
                Method = HttpMethod.Delete,
                RequestUri = new Uri(_httpClient.BaseAddress + "dimensions"),
                Content = new StringContent(JsonConvert.SerializeObject(ids), Encoding.UTF8, "application/json")
            };

            var response = await _httpClient.SendAsync(request);
            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
        }

        [Fact]
        public async Task DeleteMany_WithCorrectIds_ShouldReturn_OK()
        {
            var dimensionsResponse = await _httpClient.GetAsync("dimensions");
            var dimensions = await dimensionsResponse.Content.ReadAsAsync<List<Dimension>>();

            var dmc = new DeleteManyCommand {Ids = dimensions.TakeLast(2).Select(g => g.Id).ToArray()};

            var request = new HttpRequestMessage
            {
                Method = HttpMethod.Delete,
                RequestUri = new Uri(_httpClient.BaseAddress + "dimensions"),
                Content = new StringContent(JsonConvert.SerializeObject(dmc), Encoding.UTF8, "application/json")
            };

            var response = await _httpClient.SendAsync(request);
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }
    }
}
