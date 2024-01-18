using finshark.DTOs.Stock;
using finshark.Interfaces;
using finshark.Mappers;
using Microsoft.AspNetCore.Mvc;

namespace finshark.Controllers;

[Route("/api/stock")]
[ApiController]
public class StockController : ControllerBase
{
    private readonly IStockRepository _stockRepository;
    
    public StockController(IStockRepository stockRepository)
    {
        _stockRepository = stockRepository;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var stocks = await _stockRepository.GetAllAsync();

        return Ok(stocks);
    }

    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetById([FromRoute] int id)
    {
        var stock = await _stockRepository.GetByIdAsync(id);

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

        await _stockRepository.CreateAsync(stock);

        return CreatedAtAction(nameof(GetById), new { id = stock.Id }, stock);
    }

    [HttpPut("{id:int}")]
    public async Task<IActionResult> Update([FromRoute] int id, [FromBody] UpdateStockRequestDTO stockDTO)
    {
        var stock = await _stockRepository.UpdateAsync(id, stockDTO);

        if (stock == null)
        {
            return NotFound();
        }

        return Ok(stock);
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete([FromRoute] int id)
    {
        var stock = await _stockRepository.DeleteAsync(id);

        if (stock == null)
        {
            return NotFound();
        }

        return NoContent();
    }
}