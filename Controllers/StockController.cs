using finshark.Data;
using finshark.DTOs.Stock;
using finshark.Mappers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace finshark.Controllers;

[Route("/api/stock")]
[ApiController]
public class StockController : ControllerBase
{
    private readonly ApiDbContext _dbContext;
    
    public StockController(ApiDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var stocks = await _dbContext.Stocks.ToListAsync();

        return Ok(stocks);
    }

    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetById([FromRoute] int id)
    {
        var stock = await _dbContext.Stocks.FindAsync(id);

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
        
        await _dbContext.Stocks.AddAsync(stock);
        await _dbContext.SaveChangesAsync();

        return CreatedAtAction(nameof(GetById), new { id = stock.Id }, stock);
    }

    [HttpPut("{id:int}")]
    public async Task<IActionResult> Update([FromRoute] int id, [FromBody] UpdateStockRequestDTO stockDTO)
    {
        var stock = await _dbContext.Stocks.FirstOrDefaultAsync(s => s.Id == id);

        if (stock == null)
        {
            return NotFound();
        }

        stock.Company = stockDTO.Company;
        stock.Symbol = stockDTO.Symbol;
        stock.Industry = stock.Industry;
        stock.LastDiv = stockDTO.LastDiv;
        stock.MarketCap = stockDTO.MarketCap;
        stock.Purchase = stockDTO.Purchase;

        await _dbContext.SaveChangesAsync();

        return Ok(stock);
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete([FromRoute] int id)
    {
        var stock = await _dbContext.Stocks.FirstOrDefaultAsync(s => s.Id == id);

        if (stock == null)
        {
            return NotFound();
        }

        _dbContext.Stocks.Remove(stock);
        await _dbContext.SaveChangesAsync();

        return NoContent();
    }
}