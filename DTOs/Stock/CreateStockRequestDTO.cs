using System.ComponentModel.DataAnnotations;

namespace finshark.DTOs.Stock;

public class CreateStockRequestDTO
{
    [Required]
    [MinLength(1, ErrorMessage = "Symbol must be at least 1 char")]
    [MaxLength(8, ErrorMessage = "Symbol cannot be over 8 char")]
    public string Symbol { get; set; } = string.Empty;
    
    [Required]
    [MinLength(1, ErrorMessage = "Company must be at least 1 char")]
    [MaxLength(280, ErrorMessage = "Company cannot be over 280 char")]
    public string Company { get; set; } = string.Empty;
    
    [Required]
    [Range(1, 1_000_000_000, ErrorMessage = "Purchase must be through 1 and 1.000.000.000")]
    public decimal Purchase { get; set; }
    
    [Required]
    [Range(0.1, 100, ErrorMessage = "Last dividend must be through 0.1 and 100")]
    public decimal LastDiv { get; set; }
    
    [Required]
    [MinLength(1, ErrorMessage = "Industry segment must be at least 1 char")]
    [MaxLength(280, ErrorMessage = "Industry cannot be over 280 char")]
    public string Industry { get; set; } = string.Empty;
    
    [Required]
    [Range(1, 10_000_000_000, ErrorMessage = "Market capital must be through 1 and 10.000.000.000")]
    public long MarketCap { get; set; }
}