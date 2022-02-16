using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace BookWebApi
{
    [Route("api/[controller]")]
    [ApiController]
    public class BooksController : ControllerBase
    {
        private readonly IBookService _bookService;
        public BooksController(IBookService bookService)
        {
            _bookService = bookService;
        }
        // GET: api/<BooksController>
        [HttpGet]
        public ActionResult<IEnumerable<Book>> Get()
        {
            var items = _bookService.GetAll();
            return Ok(items);
        }

        // GET api/<BooksController>/5
        [HttpGet("{id}")]
        public ActionResult<Book> Get(Guid id)
        {
            var item = _bookService.GetById(id);
            if (item == null)
            {
                return NotFound();
            }

            return Ok(item);
        }

        // POST api/<BooksController>
        [HttpPost]
        public ActionResult Post([FromBody] Book value)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var item = _bookService.Add(value);
            return CreatedAtAction("Get", new { Id = item.Id }, item);
        }

        // DELETE api/<BooksController>/5
        [HttpDelete("{id}")]
        public ActionResult Delete(Guid id)
        {
            var existingItem = _bookService.GetById(id);
            if (existingItem == null)
            {
                return NotFound();
            }

            _bookService.Remove(id);
            return NoContent();
        }
    }
}
