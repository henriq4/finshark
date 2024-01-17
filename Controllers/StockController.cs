using finshark.Data;
using finshark.DTOs.Stock;
using finshark.Mappers;
using Microsoft.AspNetCore.Mvc;

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
    public IActionResult GetAll()
    {
        var stocks = _dbContext.Stocks.ToList();

        return Ok(stocks);
    }

    [HttpGet("{id}")]
    public IActionResult GetById([FromRoute] int id)
    {
        var stock = _dbContext.Stocks.Find(id);

        if (stock == null)
        {
            return NotFound();
        }

        return Ok(stock);
    }

    [HttpPost]
    public IActionResult Create([FromBody] CreateStockRequestDTO stockDTO)
    {
        var stock = stockDTO.ToStockFromCreateDTO();
        
        _dbContext.Stocks.Add(stock);
        _dbContext.SaveChanges();

        return CreatedAtAction(nameof(GetById), new { id = stock.Id }, stock);
    }

    [HttpPut]
    public IActionResult Update([FromRoute] int id, [FromBody] UpdateStockRequestDTO stockDTO)
    {
        var stock = _dbContext.Stocks.FirstOrDefault(s => s.Id == id);

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

        _dbContext.SaveChanges();

        return Ok(stock);
    }
}