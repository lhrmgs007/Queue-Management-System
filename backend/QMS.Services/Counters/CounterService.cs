using QMS.Core.Entities;
using QMS.Data.Repositories;

namespace QMS.Services.Counters;

public interface ICounterService
{
    Task<IEnumerable<Counter>> GetAllCountersAsync();
    Task<Counter?> GetCounterByIdAsync(int id);
    Task<Counter> CreateCounterAsync(string counterName, int serviceId, string location = "");
    Task<Counter> UpdateCounterAsync(Counter counter);
    Task DeleteCounterAsync(int id);
    Task<Counter> OpenCounterAsync(int counterId, int userId);
    Task<Counter> CloseCounterAsync(int counterId);
    Task<IEnumerable<Counter>> GetOpenCountersAsync();
}

public class CounterService : ICounterService
{
    private readonly ICounterRepository _counterRepository;
    private readonly IUserRepository _userRepository;

    public CounterService(ICounterRepository counterRepository, IUserRepository userRepository)
    {
        _counterRepository = counterRepository;
        _userRepository = userRepository;
    }

    public async Task<IEnumerable<Counter>> GetAllCountersAsync()
    {
        return await _counterRepository.GetAllCountersAsync();
    }

    public async Task<Counter?> GetCounterByIdAsync(int id)
    {
        return await _counterRepository.GetCounterByIdAsync(id);
    }

    public async Task<Counter> CreateCounterAsync(string counterName, int serviceId, string location = "")
    {
        var counter = new Counter
        {
            CounterName = counterName,
            ServiceId = serviceId,
            Location = location,
            Status = "Closed",
            IsActive = true,
            CreatedDate = DateTime.UtcNow
        };

        return await _counterRepository.AddCounterAsync(counter);
    }

    public async Task<Counter> UpdateCounterAsync(Counter counter)
    {
        return await _counterRepository.UpdateCounterAsync(counter);
    }

    public async Task DeleteCounterAsync(int id)
    {
        await _counterRepository.DeleteCounterAsync(id);
    }

    public async Task<Counter> OpenCounterAsync(int counterId, int userId)
    {
        var counter = await _counterRepository.GetCounterByIdAsync(counterId);
        if (counter == null)
            throw new InvalidOperationException($"Counter {counterId} not found");

        counter.Status = "Open";
        counter.StatusChangedDate = DateTime.UtcNow;

        var user = await _userRepository.GetUserByIdAsync(userId);
        if (user != null)
        {
            user.CounterId = counterId;
            await _userRepository.UpdateUserAsync(user);
        }

        return await _counterRepository.UpdateCounterAsync(counter);
    }

    public async Task<Counter> CloseCounterAsync(int counterId)
    {
        var counter = await _counterRepository.GetCounterByIdAsync(counterId);
        if (counter == null)
            throw new InvalidOperationException($"Counter {counterId} not found");

        counter.Status = "Closed";
        counter.StatusChangedDate = DateTime.UtcNow;

        return await _counterRepository.UpdateCounterAsync(counter);
    }

    public async Task<IEnumerable<Counter>> GetOpenCountersAsync()
    {
        return await _counterRepository.GetOpenCountersAsync();
    }
}
