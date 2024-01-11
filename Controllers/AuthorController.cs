using LibraryManagement.Entity;
using LibraryManagement.Services.Contract;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace LibraryManagement.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthorController : ControllerBase
    {

        private readonly IAuthorService _authorService;

        public AuthorController(IAuthorService authorService)
        {
            _authorService = authorService;
        }

        // GET: api/<AuthorController>
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/<AuthorController>/5
        [HttpGet("{idAuthor}")]
        public async Task<ActionResult> Get(string idAuthor)
        {
            var author = await _authorService.GetAuthorByIdAsync(idAuthor);

            if (author == null) { return NotFound(); }
            
            return Ok(author);    
        }

        // POST api/<AuthorController>
        [HttpPost]
        public async Task<ActionResult> Post([FromBody] Author author)
        {
            var addedAuthor = await _authorService.CreateAuthor(author);

            return Ok(addedAuthor);
        }

        // PUT api/<AuthorController>/5
        [HttpPut("update_author")]
        public async Task<ActionResult> Put([FromBody] Author author )
        {
           
            var updatedAuthor = await _authorService.UpdateAuthorAsync(author); 

            return Ok(updatedAuthor);   
        }

        // DELETE api/<AuthorController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
