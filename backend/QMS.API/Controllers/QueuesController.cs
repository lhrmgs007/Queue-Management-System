using Microsoft.AspNetCore.Mvc;
using QMS.Core.DTOs;
using QMS.Services.Queues;

namespace QMS.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class QueuesController : ControllerBase
{
    private readonly IQueueService _queueService;

    public QueuesController(IQueueService queueService)
    {
        _queueService = queueService;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var queues = await _queueService.GetAllQueuesAsync();
        return Ok(queues);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        var queue = await _queueService.GetQueueByIdAsync(id);
        if (queue == null)
            return NotFound();
        return Ok(queue);
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateQueueRequest request)
    {
        var queue = await _queueService.CreateQueueAsync(request.QueueName, request.Prefix, request.Color);
        return CreatedAtAction(nameof(GetById), new { id = queue.QueueId }, queue);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, [FromBody] CreateQueueRequest request)
    {
        var queue = await _queueService.GetQueueByIdAsync(id);
        if (queue == null)
            return NotFound();

        queue.QueueName = request.QueueName;
        queue.Prefix = request.Prefix;
        queue.Color = request.Color;

        var updated = await _queueService.UpdateQueueAsync(queue);
        return Ok(updated);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        await _queueService.DeleteQueueAsync(id);
        return NoContent();
    }
}
