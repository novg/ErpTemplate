using Application.Interfaces.Services;
using Application.Models;
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

    /// <summary>
    /// Get a Book's list.
    /// </summary>
    /// <returns>A Book's list.</returns>
    /// <response code="200">Returns exists books</response>
    /// <response code="500">If internal server error occured.</response>
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IEnumerable<BookDto>> GetBooks()
    {
        return await _service.GetBooks();
    }

    /// <summary>
    /// Get a Book by id.
    /// </summary>
    /// <param name="bookId">Book id for getting</param>
    /// <returns>A existing Book.</returns>
    /// <response code="200">Returns existsing book.</response>
    /// <response code="404">If book not exists.</response>
    /// <response code="500">If internal server error occured.</response>
    [HttpGet("{bookId}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<BookDto> GetBookById(int bookId)
    {
        return await _service.GetBookById(bookId);
    }
}
