using finshark.Models;

namespace finshark.Interfaces;

public interface IStockRepository
{
    public Task<List<Stock>> GetAllAsync();
}