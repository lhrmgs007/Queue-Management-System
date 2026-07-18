using Microsoft.AspNetCore.SignalR;

namespace QMS.API.Hubs;

public class QueueHub : Hub
{
    private readonly ILogger<QueueHub> _logger;

    public QueueHub(ILogger<QueueHub> logger)
    {
        _logger = logger;
    }

    public override async Task OnConnectedAsync()
    {
        _logger.LogInformation($"Client {Context.ConnectionId} connected");
        await base.OnConnectedAsync();
    }

    public override async Task OnDisconnectedAsync(Exception? exception)
    {
        _logger.LogInformation($"Client {Context.ConnectionId} disconnected");
        await base.OnDisconnectedAsync(exception);
    }

    // Device subscriptions
    public async Task SubscribeToQueue(int queueId)
    {
        await Groups.AddToGroupAsync(Context.ConnectionId, $"queue_{queueId}");
        _logger.LogInformation($"Client {Context.ConnectionId} subscribed to queue {queueId}");
    }

    public async Task UnsubscribeFromQueue(int queueId)
    {
        await Groups.RemoveFromGroupAsync(Context.ConnectionId, $"queue_{queueId}");
        _logger.LogInformation($"Client {Context.ConnectionId} unsubscribed from queue {queueId}");
    }

    // Broadcast methods (called from services)
    public async Task BroadcastTicketCalled(int queueId, int ticketId, string ticketNumber)
    {
        await Clients.Group($"queue_{queueId}")
            .SendAsync("ticket:called", new { ticketId, ticketNumber, timestamp = DateTime.UtcNow });
    }

    public async Task BroadcastTicketSkipped(int queueId, int ticketId)
    {
        await Clients.Group($"queue_{queueId}")
            .SendAsync("ticket:skipped", new { ticketId, timestamp = DateTime.UtcNow });
    }

    public async Task BroadcastTicketRecalled(int queueId, int ticketId, string ticketNumber)
    {
        await Clients.Group($"queue_{queueId}")
            .SendAsync("ticket:recalled", new { ticketId, ticketNumber, timestamp = DateTime.UtcNow });
    }

    public async Task BroadcastCounterStatusChanged(int queueId, int counterId, string status)
    {
        await Clients.Group($"queue_{queueId}")
            .SendAsync("counter:status-changed", new { counterId, status, timestamp = DateTime.UtcNow });
    }

    public async Task BroadcastQueueStats(int queueId, int waitingCount, int totalServed)
    {
        await Clients.Group($"queue_{queueId}")
            .SendAsync("queue:stats", new { queueId, waitingCount, totalServed, timestamp = DateTime.UtcNow });
    }

    public async Task BroadcastDeviceStatus(int queueId, int deviceId, string deviceType, string status)
    {
        await Clients.Group($"queue_{queueId}")
            .SendAsync("device:status", new { deviceId, deviceType, status, timestamp = DateTime.UtcNow });
    }
}
