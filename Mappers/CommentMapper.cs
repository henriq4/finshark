using finshark.DTOs.Comment;
using finshark.Models;

namespace finshark.Mappers;

public static class CommentMapper
{
   public static Comment ToCommentFromCreateCommentDTO(this CreateCommentRequestDTO commentDTO, int stockId)
   {
      return new Comment
      {
         Title = commentDTO.Title,
         Content = commentDTO.Content,
         StockId = stockId
      };
   }

   public static Comment ToCommentFromCreateCommentDTO(this UpdateCommentRequestDTO commentDTO, int stockId)
   {
      return new Comment
      {
         Title = commentDTO.Title,
         Content = commentDTO.Content,
         StockId = stockId
      };
   }
}