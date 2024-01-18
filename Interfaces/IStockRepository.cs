using finshark.DTOs.Stock;
using finshark.Models;

namespace finshark.Interfaces;

public interface IStockRepository
{
    public Task<List<Stock>> GetAllAsync();
    public Task<Stock?> GetByIdAsync(int id);
    public Task<Stock> CreateAsync(Stock stock);
    public Task<Stock?> UpdateAsync(int id, UpdateStockRequestDTO stockDTO);
    public Task<Stock?> DeleteAsync(int id);
    public Task<bool> ExistingStock(int id);
}