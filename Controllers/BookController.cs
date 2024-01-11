using LibraryManagement.Entity;
using LibraryManagement.Services.Contract;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace LibraryManagement.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookController : ControllerBase
    {
        private readonly IBookService _bookService;

        public BookController(IBookService bookService)
        {
            _bookService = bookService;
        }

        // GET: api/<BookController>
        [HttpGet]
        public async Task<ActionResult> Get()
        {
            return Ok(await _bookService.GetBooksAsync());
        }

        // GET api/<BookController>/5
        [HttpGet("{id}")]
        public async Task<ActionResult> Get(string id)
        {
            var book = await _bookService.GetBookByIdAsync(id);

            if (book == null)
            {
                return NotFound();
            }

            return Ok(book);
        }

        // POST api/<BookController>
        [HttpPost]
        public async Task<ActionResult> Post([FromBody] Book book)
        {
            var addedBook = await _bookService.CreateBook(book);

            return Ok(book);
        }

        // PUT api/<BookController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

       

        // DELETE api/<BookController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }

        // GET api/<BookController>/5
        [HttpGet("/book_category/{category}")]
        public async Task<ActionResult> GetBooksByCategory(string category)
        {
            var books = await _bookService.GetBooksByCategoryAsync(category);

            if (books == null)
            {
                return NotFound();
            }

            return Ok(books);
        }

        // GET api/<BookController>/5
        [HttpGet("/book_research_title/{title}")]
        public async Task<ActionResult> GetBookByTitle(string title)
        {
            var book = await _bookService.GetBooksByTitleAsync(title);

            if (book == null)
            {
                return NotFound();
            }

            return Ok(book);
        }
    }
}
