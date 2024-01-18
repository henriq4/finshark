using finshark.Data;
using finshark.Interfaces;
using finshark.Models;
using Microsoft.EntityFrameworkCore;

namespace finshark.Repositories;

public class CommentRepository : ICommentRepository
{
    private readonly ApiDbContext _dbContext;
    
    public CommentRepository(ApiDbContext dbContext)
    {
        _dbContext = dbContext;
    }


    public async Task<List<Comment>> GetAllAsync()
    {
        return await _dbContext.Comments.ToListAsync();
    }

    public async Task<Comment?> GetByIdAsync(int id)
    {
        return await _dbContext.Comments.FindAsync(id);
    }
}