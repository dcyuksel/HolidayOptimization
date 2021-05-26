using HolidayOptimization.Application.Interfaces.Shared;
using HolidayOptimization.Shared.Services;
using Moq;
using Newtonsoft.Json;
using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace HolidayOptimization.Shared.UnitTests.Services
{
    public class HttpClientWrapperAsyncUnitTests
    {
        private readonly string Url = "http://localhost:1234";
        private readonly IEnumerable<string> SampleData = new List<string> { "public", "holiday", "optimization" };
        private Mock<IHttpClientServiceAsync> mockHttpClientService;

        [SetUp]
        public void Setup()
        {
			var json = JsonConvert.SerializeObject(SampleData);

			HttpResponseMessage httpResponse = new HttpResponseMessage();
			httpResponse.StatusCode = System.Net.HttpStatusCode.OK;
			httpResponse.Content = new StringContent(json, Encoding.UTF8, "application/json");

			var mockHttpClientService = new Mock<IHttpClientServiceAsync>();
			mockHttpClientService.Setup(t => t.GetAsync(It.Is<string>(s => s.StartsWith(Url)))).ReturnsAsync(httpResponse);

			this.mockHttpClientService = mockHttpClientService;
		}

        [Test]
        public async Task GetAsyncTest()
        {
            var httpClientWrapperAsync = new HttpClientWrapperAsync<IEnumerable<string>>(mockHttpClientService.Object);
            var result = await httpClientWrapperAsync.GetAsync(Url);
            Assert.AreEqual(SampleData, result);
        }


        [Test]
        public async Task GetMultipleAsyncTest()
        {
            var urls = new List<string> { Url, Url, Url };
            var httpClientWrapperAsync = new HttpClientWrapperAsync<IEnumerable<string>>(mockHttpClientService.Object);
            var result = await httpClientWrapperAsync.GetMultipleAsync(urls);
            Assert.AreEqual(urls.Count, result.Count());
        }
    }
}
