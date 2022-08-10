using Bookstore.Catalog.Api.Dto.Books;
using System;
using System.Collections.Generic;
using System.Text;

namespace Bookstore.Catalog.Api.Dto.Books
{
    public class AuthorResponse
    {
        public int AuthorID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string About { get; set; }
        public byte[] Photo { get; set; }
        public int BirthYear { get; set; }
        public string Nationalty { get; set; }

     

        public ICollection<BookResponse> Books { get; set; }
    }
}
