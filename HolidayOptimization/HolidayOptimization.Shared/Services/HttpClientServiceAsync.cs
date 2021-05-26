using HolidayOptimization.Application.Interfaces.Shared;
using System.Net.Http;
using System.Threading.Tasks;

namespace HolidayOptimization.Shared.Services
{
    public class HttpClientServiceAsync : IHttpClientServiceAsync
    {
        private readonly HttpClient httpClient;

        public HttpClientServiceAsync()
        {
            httpClient = new HttpClient();
        }

        public async Task<HttpResponseMessage> GetAsync(string url)
        {
            return await httpClient.GetAsync(url);
        }

        public void Dispose()
        {
            httpClient?.Dispose();
        }
    }
}
