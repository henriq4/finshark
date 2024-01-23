using System.ComponentModel.DataAnnotations;

namespace finshark.DTOs.Comment;

public class CreateCommentRequestDTO
{
    [Required]
    [MinLength(4, ErrorMessage = "Title must be min 4 char")]
    [MaxLength(80, ErrorMessage = "Title must not be over 80 char")]
    public string Title { get; set; } = string.Empty;
    
    [Required]
    [MinLength(4, ErrorMessage = "Content must be min 4 char")]
    [MaxLength(280, ErrorMessage = "Content must not be over 280 char")]
    public string Content { get; set; } = string.Empty;
}