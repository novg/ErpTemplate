using Application.Interfaces.Services;
using Application.Models;
using Domain.Exceptions;
using Microsoft.AspNetCore.Mvc;

namespace ApiModule.Controllers;

[ApiController]
[Route("[controller]")]
public class BooksController : ControllerBase
{
    private readonly IBookService _service;
    private readonly ILogger<BooksController> _logger;

    public BooksController(IBookService service, ILogger<BooksController> logger)
    {
        _service = service;
        _logger = logger;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<BookDto>>> GetBooks()
    {
        try
        {
            IEnumerable<BookDto> books = await _service.GetBooks();
            return Ok(books);
        }
        catch (Exception ex)
        {
            _logger.LogError("Error get books: {ErrorMessage}", ex.Message);
            return Problem(detail: ex.Message);
        }
    }

    [HttpGet("{bookId}")]
    public async Task<ActionResult<IEnumerable<BookDto>>> GetBookById(int bookId)
    {
        try
        {
            BookDto book = await _service.GetBookById(bookId);
            return Ok(book);
        }
        catch (EntityNotFoundException)
        {
            _logger.LogError("Error get: book {BookId} not found", bookId);
            return NotFound();
        }
        catch (Exception ex)
        {
            _logger.LogError("Error get book {BookId}: {ErrorMessage}", bookId, ex.Message);
            return Problem(detail: ex.Message);
        }
    }
}
