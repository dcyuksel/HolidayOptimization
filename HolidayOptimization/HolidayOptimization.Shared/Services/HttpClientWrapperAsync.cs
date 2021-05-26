using HolidayOptimization.Application.Interfaces.Shared;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HolidayOptimization.Shared.Services
{
    public class HttpClientWrapperAsync<T> : IHttpClientWrapperAsync<T> where T : class
    {
        private readonly IHttpClientServiceAsync httpClientService;

        public HttpClientWrapperAsync(IHttpClientServiceAsync httpClientService)
        {
            this.httpClientService = httpClientService;
        }

        public async Task<T> GetAsync(string url)
        {
            T result = null;

            var response = await httpClientService.GetAsync(url);
            response.EnsureSuccessStatusCode();
            
            await response.Content.ReadAsStringAsync().ContinueWith((Task<string> x) =>
            {
                if (x.IsFaulted)
                    throw x.Exception;

                result = JsonConvert.DeserializeObject<T>(x.Result);
            });

            return result;
        }

        public async Task<IEnumerable<T>> GetMultipleAsync(IEnumerable<string> urls)
        {
            var requests = urls.Select(url => httpClientService.GetAsync(url)).ToList();
            await Task.WhenAll(requests);
            
            var result = new List<T>();
            foreach (var request in requests)
            {
                var httpResponseMessage = request.Result;
                var responseAsString = await httpResponseMessage.Content.ReadAsStringAsync();
                var response = JsonConvert.DeserializeObject<T>(responseAsString);
                result.Add(response);
            }

            return result;
        }
    }
}
