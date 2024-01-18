using finshark.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace finshark.Controllers;

[Route("/api/comment")]
[ApiController]
public class CommentController : ControllerBase
{
    private readonly ICommentRepository _repository;
    
    public CommentController(ICommentRepository repository)
    {
        _repository = repository;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var comments = await _repository.GetAllAsync();

        return Ok(comments);
    }

    [HttpGet]
    [Route("{id:int}")]
    public async Task<IActionResult> GetById([FromRoute] int id)
    {
        var comment = await _repository.GetByIdAsync(id);

        if (comment == null)
        {
            return NotFound();
        }

        return Ok(comment);
    }
}