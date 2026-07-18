using Microsoft.AspNetCore.Mvc;
using QMS.Core.DTOs;
using QMS.Services.Tickets;

namespace QMS.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class TicketsController : ControllerBase
{
    private readonly ITicketService _ticketService;

    public TicketsController(ITicketService ticketService)
    {
        _ticketService = ticketService;
    }

    [HttpPost("generate")]
    public async Task<IActionResult> Generate([FromBody] CreateTicketRequest request)
    {
        var ticket = await _ticketService.CreateTicketAsync(request.QueueId, request.ServiceId);
        return Ok(ticket);
    }

    [HttpGet("queue/{queueId}")]
    public async Task<IActionResult> GetByQueue(int queueId)
    {
        var tickets = await _ticketService.GetTicketsByQueueAsync(queueId);
        return Ok(tickets);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        var ticket = await _ticketService.GetTicketByIdAsync(id);
        if (ticket == null)
            return NotFound();
        return Ok(ticket);
    }

    [HttpPost("{id}/call")]
    public async Task<IActionResult> Call(int id, [FromBody] int counterId)
    {
        var ticket = await _ticketService.CallTicketAsync(id, counterId);
        return Ok(ticket);
    }

    [HttpPost("{id}/skip")]
    public async Task<IActionResult> Skip(int id)
    {
        var ticket = await _ticketService.SkipTicketAsync(id);
        return Ok(ticket);
    }

    [HttpPost("{id}/recall")]
    public async Task<IActionResult> Recall(int id)
    {
        var ticket = await _ticketService.RecallTicketAsync(id);
        return Ok(ticket);
    }

    [HttpPost("{id}/close")]
    public async Task<IActionResult> Close(int id, [FromBody] int counterId)
    {
        var ticket = await _ticketService.CloseTicketAsync(id, counterId);
        return Ok(ticket);
    }

    [HttpGet("queue/{queueId}/next-waiting")]
    public async Task<IActionResult> GetNextWaiting(int queueId)
    {
        var ticket = await _ticketService.GetNextWaitingTicketAsync(queueId);
        if (ticket == null)
            return NotFound();
        return Ok(ticket);
    }
}
