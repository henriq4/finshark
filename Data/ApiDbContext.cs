using finshark.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace finshark.Data;

public class ApiDbContext: IdentityDbContext<User>
{
    public ApiDbContext(DbContextOptions<ApiDbContext> dbContextOptions): base(dbContextOptions)
    {
        
    }
    
    public DbSet<Stock> Stocks { get; set; }
    public DbSet<Comment> Comments { get; set; }
}