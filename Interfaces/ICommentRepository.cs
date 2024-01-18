using finshark.Models;

namespace finshark.Interfaces;

public interface ICommentRepository
{
   public Task<List<Comment>> GetAllAsync();
   public Task<Comment?> GetByIdAsync(int id);
   public Task<Comment> CreateAsync(Comment comment);
   public Task<Comment?> DeleteAsync(int id);
}