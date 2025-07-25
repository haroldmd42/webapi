using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using webapi.Models;
using webapi.Services;

namespace webapi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class HistoryController : ControllerBase
{
    private readonly IAddHistoryService _addHistoryService;
    private readonly TareasContext _context;

    public HistoryController(IAddHistoryService addHistoryService, TareasContext context)
    {
        _addHistoryService = addHistoryService;
        _context = context;
    }

    // GET: api/history
    [HttpGet]
    public async Task<IActionResult> Get()
    {
        var histories = await _addHistoryService.Get();
        return Ok(histories);
    }

    // POST: api/history
    [HttpPost]
    public async Task<IActionResult> Post([FromBody] AddHistory request)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        try
        {
            var result = await _addHistoryService.Save(request);
            return Ok(new
            {
                message = "Historias guardadas correctamente.",
                accessCode = result.accessCode,
                estimationId = result.estimationId
            });
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { message = "Error al guardar historias", details = ex.Message });
        }
    }

    // GET: api/history/by-code/ABC123
    [HttpGet("by-code/{code}")]
    public async Task<IActionResult> GetByAccessCode(string code)
    {
        var histories = await _context.Histories
            .Where(h => h.AccessCode == code)
            .ToListAsync();

        if (!histories.Any())
            return NotFound("No se encontró ninguna estimación con ese código.");

        return Ok(histories);
    }
}
