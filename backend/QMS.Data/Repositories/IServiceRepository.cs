using QMS.Core.Entities;

namespace QMS.Data.Repositories;

public interface IServiceRepository
{
    Task<IEnumerable<Service>> GetServicesByQueueAsync(int queueId);
    Task<Service?> GetServiceByIdAsync(int id);
    Task<Service> AddServiceAsync(Service service);
    Task<Service> UpdateServiceAsync(Service service);
    Task DeleteServiceAsync(int id);
}
