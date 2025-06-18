using AutoMapper;
using DTOs;
using Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Models;


namespace Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BookController : ControllerBase
    {
        private readonly IBookService _bookService;
        private readonly IMapper _mapper;


        public BookController(IBookService bookService, IMapper mapper)
        {
            _bookService = bookService;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll() => Ok(await _bookService.GetAll());

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var book = await _bookService.GetById(id);
            return book == null ? NotFound() : Ok(book);
        }

        [HttpGet("search")]
        public async Task<IActionResult> Search([FromQuery] SearchBookDto dto)
        {
            if (dto.Title.Contains("<script>", StringComparison.OrdinalIgnoreCase))
                throw new BadHttpRequestException("Title contains invalid content.");
            var results = await _bookService.SearchBooks(dto.Title, dto.Author, dto.Genre);
            return Ok(results);
        }
        [Authorize]
        [HttpPost]
        public async Task<IActionResult> Create(BookDto bookDto)
        {
            var book = _mapper.Map<Book>(bookDto);
            var created = await _bookService.Add(book);
            var resultDto = _mapper.Map<BookDto>(created);
            return CreatedAtAction(nameof(GetById), new { id = created.Id }, resultDto);
        }

        [Authorize]
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(Guid id, Book book)
        {
            var updated = await _bookService.Update(id, book);
            return updated == null ? NotFound() : Ok(updated);
        }

        [Authorize]
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var success = await _bookService.Delete(id);
            return success ? NoContent() : NotFound();
        }
    }
}

