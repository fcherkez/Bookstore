using AutoMapper;
using Bookstore.Catalog.Api.Context;
using Bookstore.Catalog.Api.Dto.Books;

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Bookstore.Catalog.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
   
    public class AuthorsController : ControllerBase
    {
        private readonly CatalogDbContext _context;
        private readonly IMapper _mapper;
        public AuthorsController(CatalogDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        [HttpGet(Name = "GetAllAuthors")]
        public async Task<ActionResult<IEnumerable<AuthorResponse>>> Get()
        {
            var authors = await _context.Authors
                .OrderBy(x => x.FirstName)
                .ToListAsync();


            var authorResponses = _mapper.Map<List<AuthorResponse>>(authors);
            return Ok(authorResponses);
        }

        [HttpGet("{id}", Name = "GetAuthor")]
        public async Task<ActionResult<AuthorResponse>> Get(int id)
        {
            var author = await _context.Authors

                .Include(x => x.Books)
                .ThenInclude(x => x.Book)
                .SingleOrDefaultAsync(x => x.AuthorID == id);
            if (author == null)
            {
                return NotFound();
            }


            var authorResponses = _mapper.Map<AuthorResponse>(author);
            return Ok(authorResponses);
        }
    }
}
