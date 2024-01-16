using finshark.Data;
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
}