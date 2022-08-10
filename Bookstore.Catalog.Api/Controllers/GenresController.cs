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
    public class GenresController : ControllerBase
    {
      
        
            private readonly CatalogDbContext _context;
            private readonly IMapper _mapper;
            public GenresController(CatalogDbContext context, IMapper mapper)
            {
                _context = context;
                _mapper = mapper;
            }

            [HttpGet(Name = "GetAllGenres")]
            public async Task<ActionResult<IEnumerable<GenreResponse>>> Get()
            {
                var genres = await _context.Genres
                    .ToListAsync();


                var genreResponses = _mapper.Map<List<GenreResponse>>(genres);
                return Ok(genreResponses);
            }

        [HttpGet("{id}", Name = "GetGenre")]
        public async Task<ActionResult<GenreResponse>> Get(int id)
        {
            var genre = await _context.Genres

                .Include(x => x.Books)
                .ThenInclude(x => x.Book)
                .SingleOrDefaultAsync(x => x.GenreID == id);
            if (genre == null)
            {
                return NotFound();
            }


            var genreResponses = _mapper.Map<GenreResponse>(genre);
            return Ok(genreResponses);
        }


    }
    }

