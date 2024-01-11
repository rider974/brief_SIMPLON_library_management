using LibraryManagement.Entity;
using LibraryManagement.Services.Contract;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace LibraryManagement.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BorrowerController : ControllerBase
    {

        private readonly IBookService _bookService;

        private readonly IBorrowerService _borrowerService;
        public BorrowerController(IBookService bookService, IBorrowerService borrowerService)
        {
            _bookService = bookService;
            _borrowerService = borrowerService; 
        }


        // GET: api/<BorrowerController>
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/<BorrowerController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<BorrowerController>
        [HttpPost]
        public async Task<ActionResult> Post([FromBody] Borrower borrower)
        {

            var addedBorrower = await _borrowerService.CreateBorrower(borrower);

            return Ok(addedBorrower);

        }

        // PUT api/<BorrowerController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // Emprunter un livre api/<BorrowerController>/5
        [HttpPut("borrow_book/{idBookToBorrow}/{idBorrower}")]
        public async Task<ActionResult> BorrowBook(string idBookToBorrow, string idBorrower)
        {
            var isBookAvailable = await _bookService.GetBookByIdAsync(idBookToBorrow);

            if(isBookAvailable.isAvailable == false ) { return NotFound();  }


            var isBorrowed =  await _borrowerService.BorrowBook(idBookToBorrow, idBorrower);

            if (isBorrowed) return Ok();
            else return NotFound();
        }


        // PUT api/<BorrowerController>/5
        [HttpPut("return_book/{idBookToReturn}/{idBorrower}")]
        public async Task<ActionResult> ReturnBook(string idBookToReturn, string idBorrower)
        {
            var isBookAvailable = await _bookService.GetBookByIdAsync(idBookToReturn);

            if (isBookAvailable.isAvailable == true) { return NotFound(); }


            await _borrowerService.ReturnBook(idBookToReturn, idBorrower);

            return Ok();
        }

        // GET api/<BorrowerController>/5
        [HttpGet("count_per_book")]
        public async Task<ActionResult> GetNumberOfBorrowingPerBook()
        {
            Dictionary<string, int> bookPerNumberTimesBorrowed = new Dictionary<string, int>();

            bookPerNumberTimesBorrowed = await _borrowerService.GetBooksByTotalBorrowing();

            if (bookPerNumberTimesBorrowed == null) return NotFound();

            return Ok(bookPerNumberTimesBorrowed);
        }

        // DELETE api/<BorrowerController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
