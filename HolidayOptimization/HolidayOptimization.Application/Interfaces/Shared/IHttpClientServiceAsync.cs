using System.Net.Http;
using System.Threading.Tasks;

namespace HolidayOptimization.Application.Interfaces.Shared
{
    public interface IHttpClientServiceAsync 
    {
        Task<HttpResponseMessage> GetAsync(string url);
    }
}
