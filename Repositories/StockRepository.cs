using finshark.Data;
using finshark.Interfaces;
using finshark.Models;
using Microsoft.EntityFrameworkCore;

namespace finshark.Repositories;

public class StockRepository : IStockRepository
{
    private readonly ApiDbContext _dbContext;

    public StockRepository(ApiDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    
    public async Task<List<Stock>> GetAllAsync()
    {
        return await _dbContext.Stocks.ToListAsync();
    }
}