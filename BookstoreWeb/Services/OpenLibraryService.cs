using Bookstore.Web.OpenLibrary;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace Bookstore.Web.Services
{
    public class OpenLibraryService
    {
        public HttpClient Client { get; set; }

        public OpenLibraryService(HttpClient client)
        {
            this.Client = client;
        }




        public async Task<Book> GetBookByISBN(string isbn)
        {
            var response = await Client.GetAsync($"books?bibkeys=ISBN:{isbn}&format=json&jscmd=data");
            response.EnsureSuccessStatusCode();

            var bookResponse = await response.Content.ReadAsStringAsync();


            var dictionary = JsonConvert.DeserializeObject<Dictionary<string, Book>>(bookResponse);
            var book = dictionary.First().Value;

            return book;


        }





    }
}
