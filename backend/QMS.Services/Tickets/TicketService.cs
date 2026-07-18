using QMS.Core.Entities;
using QMS.Data.Repositories;

namespace QMS.Services.Tickets;

public interface ITicketService
{
    Task<Ticket> CreateTicketAsync(int queueId, int serviceId);
    Task<IEnumerable<Ticket>> GetTicketsByQueueAsync(int queueId);
    Task<Ticket?> GetTicketByIdAsync(int id);
    Task<Ticket?> GetNextWaitingTicketAsync(int queueId);
    Task<Ticket> CallTicketAsync(int ticketId, int counterId);
    Task<Ticket> SkipTicketAsync(int ticketId);
    Task<Ticket> RecallTicketAsync(int ticketId);
    Task<Ticket> CloseTicketAsync(int ticketId, int counterId);
}

public class TicketService : ITicketService
{
    private readonly ITicketRepository _ticketRepository;
    private readonly IQueueService _queueService;

    public TicketService(ITicketRepository ticketRepository, IQueueService queueService)
    {
        _ticketRepository = ticketRepository;
        _queueService = queueService;
    }

    public async Task<Ticket> CreateTicketAsync(int queueId, int serviceId)
    {
        var ticketNumber = await _queueService.GenerateTicketNumberAsync(queueId);
        var ticket = new Ticket
        {
            QueueId = queueId,
            TicketNumber = ticketNumber,
            ServiceId = serviceId,
            Status = "Waiting",
            CreatedDate = DateTime.UtcNow,
            ExpiryDate = DateTime.UtcNow.AddHours(1),
            CallCount = 0
        };

        return await _ticketRepository.CreateTicketAsync(ticket);
    }

    public async Task<IEnumerable<Ticket>> GetTicketsByQueueAsync(int queueId)
    {
        return await _ticketRepository.GetTicketsByQueueAsync(queueId);
    }

    public async Task<Ticket?> GetTicketByIdAsync(int id)
    {
        return await _ticketRepository.GetTicketByIdAsync(id);
    }

    public async Task<Ticket?> GetNextWaitingTicketAsync(int queueId)
    {
        return await _ticketRepository.GetNextWaitingTicketAsync(queueId);
    }

    public async Task<Ticket> CallTicketAsync(int ticketId, int counterId)
    {
        var ticket = await _ticketRepository.GetTicketByIdAsync(ticketId);
        if (ticket == null)
            throw new InvalidOperationException($"Ticket {ticketId} not found");

        ticket.Status = "Called";
        ticket.CallCount++;
        ticket.CounterId = counterId;

        return await _ticketRepository.UpdateTicketAsync(ticket);
    }

    public async Task<Ticket> SkipTicketAsync(int ticketId)
    {
        var ticket = await _ticketRepository.GetTicketByIdAsync(ticketId);
        if (ticket == null)
            throw new InvalidOperationException($"Ticket {ticketId} not found");

        ticket.Status = "Waiting";
        return await _ticketRepository.UpdateTicketAsync(ticket);
    }

    public async Task<Ticket> RecallTicketAsync(int ticketId)
    {
        var ticket = await _ticketRepository.GetTicketByIdAsync(ticketId);
        if (ticket == null)
            throw new InvalidOperationException($"Ticket {ticketId} not found");

        ticket.Status = "Called";
        return await _ticketRepository.UpdateTicketAsync(ticket);
    }

    public async Task<Ticket> CloseTicketAsync(int ticketId, int counterId)
    {
        var ticket = await _ticketRepository.GetTicketByIdAsync(ticketId);
        if (ticket == null)
            throw new InvalidOperationException($"Ticket {ticketId} not found");

        ticket.Status = "Served";
        ticket.CounterId = counterId;

        return await _ticketRepository.UpdateTicketAsync(ticket);
    }
}
