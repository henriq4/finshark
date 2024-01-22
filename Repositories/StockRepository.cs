using finshark.Data;
using finshark.DTOs.Stock;
using finshark.Helpers;
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
    
    public async Task<List<Stock>> GetAllAsync(QueryObject query)
    {
        var stocks = _dbContext.Stocks.AsQueryable();

        if (!string.IsNullOrWhiteSpace(query.Comapny))
        {
            stocks = stocks.Where(s => s.Company == query.Comapny);
        }

        if (!string.IsNullOrWhiteSpace(query.Symbol))
        {
            stocks = stocks.Where(s => s.Symbol == query.Symbol);
        }

        return await stocks.ToListAsync();
    }

    public async Task<Stock?> GetByIdAsync(int id)
    {
        return await _dbContext.Stocks.Include(c => c.Comments).FirstOrDefaultAsync(s => s.Id == id);
    }

    public async Task<Stock> CreateAsync(Stock stock)
    {
        await _dbContext.Stocks.AddAsync(stock);
        await _dbContext.SaveChangesAsync();

        return stock;
    }

    public async Task<Stock?> UpdateAsync(int id, UpdateStockRequestDTO stockDTO)
    {
         var stock = await _dbContext.Stocks.FirstOrDefaultAsync(s => s.Id == id);
        
         if (stock == null)
         {
             return null;
         }
        
         stock.Company = stockDTO.Company;
         stock.Symbol = stockDTO.Symbol;
         stock.Industry = stock.Industry;
         stock.LastDiv = stockDTO.LastDiv;
         stock.MarketCap = stockDTO.MarketCap;
         stock.Purchase = stockDTO.Purchase;

         await _dbContext.SaveChangesAsync();
         
         return stock;
    }

    public async Task<Stock?> DeleteAsync(int id)
    {
        var stock = await _dbContext.Stocks.FirstOrDefaultAsync(s => s.Id == id);
        
        if (stock == null)
        {
            return null;
        }
        
        _dbContext.Stocks.Remove(stock);
        await _dbContext.SaveChangesAsync();

        return stock;
    }

    public async Task<bool> ExistingStock(int id)
    {
        return await _dbContext.Stocks.AnyAsync(s => s.Id == id);
    }
}