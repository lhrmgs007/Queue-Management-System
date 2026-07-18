using Microsoft.AspNetCore.Mvc;
using QMS.Core.DTOs;
using QMS.Services.Counters;

namespace QMS.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CountersController : ControllerBase
{
    private readonly ICounterService _counterService;

    public CountersController(ICounterService counterService)
    {
        _counterService = counterService;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var counters = await _counterService.GetAllCountersAsync();
        return Ok(counters);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        var counter = await _counterService.GetCounterByIdAsync(id);
        if (counter == null)
            return NotFound();
        return Ok(counter);
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateCounterRequest request)
    {
        var counter = await _counterService.CreateCounterAsync(request.CounterName, request.ServiceId, request.Location);
        return CreatedAtAction(nameof(GetById), new { id = counter.CounterId }, counter);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, [FromBody] CreateCounterRequest request)
    {
        var counter = await _counterService.GetCounterByIdAsync(id);
        if (counter == null)
            return NotFound();

        counter.CounterName = request.CounterName;
        counter.ServiceId = request.ServiceId;
        counter.Location = request.Location;

        var updated = await _counterService.UpdateCounterAsync(counter);
        return Ok(updated);
    }

    [HttpPost("{id}/open")]
    public async Task<IActionResult> Open(int id, [FromBody] int userId)
    {
        var counter = await _counterService.OpenCounterAsync(id, userId);
        return Ok(counter);
    }

    [HttpPost("{id}/close")]
    public async Task<IActionResult> Close(int id)
    {
        var counter = await _counterService.CloseCounterAsync(id);
        return Ok(counter);
    }

    [HttpGet("open")]
    public async Task<IActionResult> GetOpen()
    {
        var counters = await _counterService.GetOpenCountersAsync();
        return Ok(counters);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        await _counterService.DeleteCounterAsync(id);
        return NoContent();
    }
}
