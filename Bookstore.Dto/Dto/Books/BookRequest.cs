using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Bookstore.Catalog.Api.Dto.Books
{
    public class BookRequest
    {
        
        public string ISBN { get; set; }
        public string Title { get; set; }
        public int? Year { get; set; }
        public int? PublisherID { get; set; }
        public int? LanguageID { get; set; }
        public decimal? Price { get; set; }

        public IList<int> Authors { get; set; }
        public IList<int> Genres { get; set; }

    }
}
