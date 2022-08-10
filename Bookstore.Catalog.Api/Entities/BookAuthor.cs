using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Bookstore.Catalog.Api.Entities
{
    public class BookAuthor
    {
        public int BookID { get; set; }
        public int AuthorID { get; set; }
        public int Position { get; set; }

        public Book Book { get; set; }
        public Author Author { get; set; }
    }
    
}
