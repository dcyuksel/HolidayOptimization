using System.Collections.Generic;
using System.Threading.Tasks;

namespace HolidayOptimization.Application.Interfaces.Shared
{
    public interface IHttpClientWrapperAsync<T> where T : class
    {
        Task<T> GetAsync(string url);
        Task<IEnumerable<T>> GetMultipleAsync(IEnumerable<string> urls);
    }
}
