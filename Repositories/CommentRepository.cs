using finshark.Data;
using finshark.DTOs.Comment;
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

    public async Task<Comment> CreateAsync(Comment comment)
    {
        await _dbContext.Comments.AddAsync(comment);
        await _dbContext.SaveChangesAsync();

        return comment;
    }

    public async Task<Comment?> DeleteAsync(int id)
    {
        var comment = await _dbContext.Comments.FirstOrDefaultAsync(c => c.Id == id);

        if (comment == null)
        {
            return null;
        }

        _dbContext.Comments.Remove(comment);
        await _dbContext.SaveChangesAsync();

        return comment;
    }

    public async Task<Comment?> UpdateAsync(int id, Comment commentRequest)
    {
        var comment = await _dbContext.Comments.FirstOrDefaultAsync(c => c.Id == id);
        
        if (comment == null)
        {
            return null;
        }
        
        comment.Title = commentRequest.Title;
        comment.Content = commentRequest.Content;
        
        await _dbContext.SaveChangesAsync();
        
        return comment;
    }
}