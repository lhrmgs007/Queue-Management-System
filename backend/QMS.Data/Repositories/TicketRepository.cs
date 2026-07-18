using Microsoft.EntityFrameworkCore;
using QMS.Core.Entities;
using QMS.Data.Context;

namespace QMS.Data.Repositories;

public class TicketRepository : ITicketRepository
{
    private readonly QmsDbContext _context;

    public TicketRepository(QmsDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Ticket>> GetTicketsByQueueAsync(int queueId)
    {
        return await _context.Tickets
            .Include(t => t.Queue)
            .Include(t => t.Service)
            .Where(t => t.QueueId == queueId)
            .OrderByDescending(t => t.CreatedDate)
            .ToListAsync();
    }

    public async Task<Ticket?> GetTicketByIdAsync(int id)
    {
        return await _context.Tickets
            .Include(t => t.Queue)
            .Include(t => t.Service)
            .Include(t => t.Histories)
            .Include(t => t.Calls)
            .FirstOrDefaultAsync(t => t.TicketId == id);
    }

    public async Task<Ticket?> GetTicketByNumberAsync(string ticketNumber)
    {
        return await _context.Tickets
            .Include(t => t.Queue)
            .Include(t => t.Service)
            .FirstOrDefaultAsync(t => t.TicketNumber == ticketNumber);
    }

    public async Task<Ticket> CreateTicketAsync(Ticket ticket)
    {
        _context.Tickets.Add(ticket);
        await _context.SaveChangesAsync();
        return ticket;
    }

    public async Task<Ticket> UpdateTicketAsync(Ticket ticket)
    {
        _context.Tickets.Update(ticket);
        await _context.SaveChangesAsync();
        return ticket;
    }

    public async Task<Ticket?> GetNextWaitingTicketAsync(int queueId)
    {
        return await _context.Tickets
            .Include(t => t.Service)
            .Where(t => t.QueueId == queueId && t.Status == "Waiting")
            .OrderBy(t => t.CreatedDate)
            .FirstOrDefaultAsync();
    }

    public async Task<IEnumerable<Ticket>> GetTicketHistoryAsync(int queueId, DateTime fromDate, DateTime toDate)
    {
        return await _context.Tickets
            .Where(t => t.QueueId == queueId && t.CreatedDate >= fromDate && t.CreatedDate <= toDate)
            .OrderByDescending(t => t.CreatedDate)
            .ToListAsync();
    }
}
