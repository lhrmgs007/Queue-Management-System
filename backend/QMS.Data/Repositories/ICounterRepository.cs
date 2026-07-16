using QMS.Core.Entities;

namespace QMS.Data.Repositories;

public interface ICounterRepository
{
    Task<IEnumerable<Counter>> GetAllCountersAsync();
    Task<Counter?> GetCounterByIdAsync(int id);
    Task<Counter> AddCounterAsync(Counter counter);
    Task<Counter> UpdateCounterAsync(Counter counter);
    Task DeleteCounterAsync(int id);
    Task<IEnumerable<Counter>> GetCountersByServiceAsync(int serviceId);
    Task<IEnumerable<Counter>> GetOpenCountersAsync();
}
