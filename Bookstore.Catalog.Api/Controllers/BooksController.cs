using AutoMapper;
using Bookstore.Catalog.Api.Context;
using Bookstore.Catalog.Api.Dto.Books;
using Bookstore.Catalog.Api.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Bookstore.Catalog.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Produces("application/json")]
    public class BooksController : ControllerBase
    {
        private readonly CatalogDbContext _context;
        private readonly IMapper _mapper;
        public BooksController(CatalogDbContext context, IMapper mapper) 
        {
            _context = context;
            _mapper = mapper;
        }
        /// <summary>
        ///Returns all books 
        /// </summary>
        /// <returns>all books</returns>
        [HttpGet(Name = "GetAll")]
        public async Task<ActionResult<IEnumerable<BookResponse>>> Get()
        {
            var books = await _context.Books
                .Include(x => x.Publisher)
                .Include(x => x.Language)
                .Include(x => x.Authors)
                .ThenInclude(x => x.Author)
                .Include(x => x.Genres)
                .ThenInclude(x => x.Genre)
                .ToListAsync();

        
            var bookResponses = _mapper.Map<List<BookResponse>>(books);
            return Ok(bookResponses);
        }

        /// <summary>
        /// Returns a single book by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}", Name ="Get")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        
        public async Task<ActionResult<Book>> Get(int id)
        {
            var book = await _context.Books.FindAsync(id);
            if (book == null)
            {
                return NotFound();
            }
            var bookResponse = _mapper.Map<BookResponse>(book);

            return Ok(bookResponse);
        }

        [HttpPost(Name ="Create")]
        [Consumes("application/json")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public async Task<ActionResult<BookResponse>> Post(BookRequest bookRequest)
        {
            var book = _mapper.Map<Book>(bookRequest);
            _context.Books.Add(book);
            await _context.SaveChangesAsync();


            var bookResponse = _mapper.Map<BookResponse>(book);
            return CreatedAtAction("Get", new {id = bookResponse.BookID }, bookResponse);
        }

        [HttpPut("{bookId}", Name="Update")]
        [Consumes("application/json")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]

        public async Task<ActionResult<BookResponse>> Put(int bookId, BookRequest bookRequest)
        {
            var originalBook = await _context.Books.FindAsync(bookId);
            if (originalBook == null)
            {
                return NotFound();
            }

            _mapper.Map(bookRequest, originalBook);
            _context.Entry(originalBook).State = EntityState.Modified;

            await _context.SaveChangesAsync();

            var bookResponse = _mapper.Map<BookResponse>(originalBook);
            return Ok(bookResponse);
        }

        [HttpPatch("{bookId}", Name ="Update1")]
        [Consumes("application/json-patch+json")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]

        public async Task<ActionResult<BookResponse>> Patch(int bookId,[FromBody] JsonPatchDocument<BookRequest> bookRequestPatch)
        {
            var originalBook = await _context.Books.FindAsync(bookId);
            if (originalBook == null)
            {
                return NotFound();
            }

            var bookRequest = _mapper.Map<BookRequest>(originalBook);
            bookRequestPatch.ApplyTo(bookRequest);

            _mapper.Map(bookRequest, originalBook);
            _context.Entry(originalBook).State = EntityState.Modified;

            await _context.SaveChangesAsync();

            var bookResponse = _mapper.Map<BookResponse>(originalBook);
            return Ok(bookResponse);
        }

        [HttpDelete("{bookId}", Name ="Delete")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]

        public async Task<ActionResult> Delete (int bookId)
        {
            var book = await _context.Books.FindAsync(bookId);
            if (book == null)
            {
                return NotFound();
            }

            
            _context.Books.Remove(book);
            await _context.SaveChangesAsync();

            return Ok();
        }
    }
}
