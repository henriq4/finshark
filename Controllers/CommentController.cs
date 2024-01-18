using finshark.DTOs.Comment;
using finshark.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace finshark.Controllers;

[Route("/api/comment")]
[ApiController]
public class CommentController : ControllerBase
{
    private readonly ICommentRepository _commentRepository;
    
    public CommentController(ICommentRepository commentRepository)
    {
        _commentRepository = commentRepository;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var comments = await _commentRepository.GetAllAsync();

        return Ok(comments);
    }

    [HttpGet]
    [Route("{id:int}")]
    public async Task<IActionResult> GetById([FromRoute] int id)
    {
        var comment = await _commentRepository.GetByIdAsync(id);

        if (comment == null)
        {
            return NotFound();
        }

        return Ok(comment);
    }

    [HttpPost("{stockId:int}")]
    public async Task<IActionResult> Create([FromRoute] int stockId, CreateCommentRequestDTO commentDTO)
    {
        return Ok();
    }
}