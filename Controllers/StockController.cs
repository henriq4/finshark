using finshark.DTOs.Stock;
using finshark.Interfaces;
using finshark.Mappers;
using Microsoft.AspNetCore.Mvc;

namespace finshark.Controllers;

[Route("/api/stock")]
[ApiController]
public class StockController : ControllerBase
{
    private readonly IStockRepository _repository;
    
    public StockController(IStockRepository repository)
    {
        _repository = repository;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var stocks = await _repository.GetAllAsync();

        return Ok(stocks);
    }

    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetById([FromRoute] int id)
    {
        var stock = await _repository.GetByIdAsync(id);

        if (stock == null)
        {
            return NotFound();
        }

        return Ok(stock);
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateStockRequestDTO stockDTO)
    {
        var stock = stockDTO.ToStockFromCreateDTO();

        await _repository.CreateAsync(stock);

        return CreatedAtAction(nameof(GetById), new { id = stock.Id }, stock);
    }

    [HttpPut("{id:int}")]
    public async Task<IActionResult> Update([FromRoute] int id, [FromBody] UpdateStockRequestDTO stockDTO)
    {
        var stock = await _repository.UpdateAsync(id, stockDTO);

        if (stock == null)
        {
            return NotFound();
        }

        return Ok(stock);
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete([FromRoute] int id)
    {
        var stock = await _repository.DeleteAsync(id);

        if (stock == null)
        {
            return NotFound();
        }

        return NoContent();
    }
}