using finshark.DTOs.Stock;
using finshark.Models;

namespace finshark.Mappers;

public static class StockMapper
{
    public static Stock ToStockFromCreateDTO(this CreateStockRequestDTO stock)
    {
        return new Stock
        { 
            Symbol = stock.Symbol,
            Company = stock.Company,
            MarketCap = stock.MarketCap,
            Purchase = stock.Purchase,
            Industry = stock.Industry,
            LastDiv = stock.LastDiv,
        };
    }
}