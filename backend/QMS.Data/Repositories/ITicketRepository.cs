using QMS.Core.Entities;

namespace QMS.Data.Repositories;

public interface ITicketRepository
{
    Task<IEnumerable<Ticket>> GetTicketsByQueueAsync(int queueId);
    Task<Ticket?> GetTicketByIdAsync(int id);
    Task<Ticket?> GetTicketByNumberAsync(string ticketNumber);
    Task<Ticket> CreateTicketAsync(Ticket ticket);
    Task<Ticket> UpdateTicketAsync(Ticket ticket);
    Task<Ticket?> GetNextWaitingTicketAsync(int queueId);
    Task<IEnumerable<Ticket>> GetTicketHistoryAsync(int queueId, DateTime fromDate, DateTime toDate);
}
