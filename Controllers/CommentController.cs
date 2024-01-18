using finshark.DTOs.Comment;
using finshark.Interfaces;
using finshark.Mappers;
using Microsoft.AspNetCore.Mvc;

namespace finshark.Controllers;

[Route("/api/comment")]
[ApiController]
public class CommentController : ControllerBase
{
    private readonly ICommentRepository _commentRepository;
    private readonly IStockRepository _stockRepository;
    
    public CommentController(ICommentRepository commentRepository, IStockRepository stockRepository)
    {
        _commentRepository = commentRepository;
        _stockRepository = stockRepository;
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
        if (!await _stockRepository.ExistingStock(stockId))
        {
            return BadRequest("Stock does not exist");
        }

        var comment = commentDTO.ToCommentFromCreateCommentDTO(stockId);
        
        await _commentRepository.Create(comment);

        return CreatedAtAction(nameof(GetById), new { id = comment.Id }, comment);
    }
}