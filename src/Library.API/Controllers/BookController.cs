using Library.API.Model;
using Library.Core.Contracts;
using Library.Core.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Library.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Produces("application/json")]
    public class BookController : ControllerBase
    {
        private readonly IBookService _service;

        private readonly ILogger _logger;
        
        public BookController(ILogger<BookController> logger, IBookService service)
        {
            _service = service ?? throw new ArgumentNullException(nameof(service));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        /// <summary>
        /// Retrieve all books
        /// </summary>
        /// <remarks>This endpoint is responsible for retrieving all books from the shelf</remarks>
        /// <response code="200">books found</response>
        [HttpGet]
        [Route("/api/book")]
        [ProducesResponseType(typeof(IEnumerable<Book>), StatusCodes.Status200OK)]
        public async Task<IActionResult> Get()
        {
            using (_logger.BeginScope("[GET] /api/book"))
            {
                var books = await _service.GetAll();
                return Ok(books);
            }
        }

        /// <summary>
        /// Retrieve the book by Id
        /// </summary>
        /// <remarks>This endpoint is responsible for retrieving book by Id</remarks>
        /// <param name="id">Desired book id</param>
        /// <response code="200">book found</response>
        /// <response code="404">book not found</response>
        [HttpGet]
        [Route("/api/book/{id}")]
        [ProducesResponseType(typeof(Book), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetById([FromRoute] int id)
        {
            using (_logger.BeginScope($"[GET] /api/book/{id}"))
            {
                try
                {
                    var book = await _service.GetById(id);
                    return Ok(book);
                }
                catch (IndexOutOfRangeException e)
                {
                    _logger.LogInformation(e, $"Book {id} not found");
                    return NotFound();
                }
            }
        }

        /// <summary>
        /// Create new book
        /// </summary>
        /// <remarks>This endpoint is responsible for registering a new book</remarks>
        /// <param name="book">book</param>
        /// <response code="201">book created</response>
        /// <response code="409">book already exists</response>
        [HttpPost]
        [Route("/api/book")]
        [ProducesResponseType(typeof(BaseModel), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status409Conflict)]
        public async Task<IActionResult> Post([FromBody] Book book)
        {
            using (_logger.BeginScope("[POST] /api/book"))
            {
                try
                {
                    var bookId = await _service.Create(book);
                    return Created(nameof(Get), new { id = bookId });
                }
                catch (ArrayTypeMismatchException e)
                {
                    _logger.LogInformation(e, "Book already exists");
                    return Conflict();
                }
            }
        }

        /// <summary>
        /// Update a book
        /// </summary>
        /// <remarks>This endpoint is responsible for updating a book</remarks>
        /// <param name="book">book with updated properties</param>
        /// <param name="id">book Id</param>
        /// <response code="204">book updated</response>
        /// <response code="404">book not found</response>
        [HttpPut]
        [Route("/api/book/{id}")]
        [ProducesResponseType(typeof(BaseModel), StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Put([FromRoute] int id, [FromBody] Book book)
        {
            using (_logger.BeginScope($"[PUT] /api/book/{id}"))
            {
                try
                {
                    await _service.Update(id, book);
                    return NoContent();
                }
                catch (IndexOutOfRangeException e)
                {
                    _logger.LogInformation(e, $"Book {id} not found");
                    return NotFound();
                }
            }
        }

        /// <summary>
        /// Retrieve all books filtered by query
        /// </summary>
        /// <remarks>This endpoint is responsible for retrieving all books from the shelf based on the Author, Title, Genre and ISBN</remarks>
        /// <param name="query">Desired query</param>
        /// <response code="200">books found</response>
        /// <response code="404">book not found</response>
        [HttpGet]
        [Route("/api/book/search")]
        [ProducesResponseType(typeof(IEnumerable<Book>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Search([FromQuery] string query)
        {
            using (_logger.BeginScope($"[GET] /api/book/search?query={query}"))
            {
                try
                {
                    var books = await _service.Search(query);
                    return Ok(books);
                }
                catch (IndexOutOfRangeException e)
                {
                    _logger.LogInformation(e, $"Book not found based on the query {query}");
                    return NotFound();
                }
            }
        }

    }
}
