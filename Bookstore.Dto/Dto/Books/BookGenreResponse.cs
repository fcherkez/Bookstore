using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Bookstore.Catalog.Api.Dto.Books
{
    public class BookGenreResponse
    {
        public int GenreID { get; set; }
        public string GenreName { get; set; }
    }
}
