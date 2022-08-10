using Bookstore.Catalog.Api.Dto.Books;
using System.Collections.Generic;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;

namespace BookstoreWeb.Services
{
    public class BookCatalogService
    {
        public HttpClient Client { get; set; }
        public BookCatalogService(HttpClient client)
        {
            this.Client = client;
        }

        public async Task<List<BookResponse>> GetAllBooks()
        {
            var booksResponse = await Client.GetAsync("books");
            booksResponse.EnsureSuccessStatusCode();

            var stream = await booksResponse.Content.ReadAsStreamAsync();
            var books = await JsonSerializer.DeserializeAsync<List<BookResponse>>
                (stream, new JsonSerializerOptions() {PropertyNameCaseInsensitive = true });
            return books;
        }

        public async Task<List<AuthorResponse>> GetAllAuthors()
        {
            var authorsResponse = await Client.GetAsync("authors");
            authorsResponse.EnsureSuccessStatusCode();

            var stream = await authorsResponse.Content.ReadAsStreamAsync();
            var authors = await JsonSerializer.DeserializeAsync<List<AuthorResponse>>
                (stream, new JsonSerializerOptions() { PropertyNameCaseInsensitive = true });
            return authors;
        }

        public async Task<List<GenreResponse>> GetAllGenres()
        {
            var genresResponse = await Client.GetAsync("genres");
            genresResponse.EnsureSuccessStatusCode();

            var stream = await genresResponse.Content.ReadAsStreamAsync();
            var genres = await JsonSerializer.DeserializeAsync<List<GenreResponse>>
                (stream, new JsonSerializerOptions() { PropertyNameCaseInsensitive = true });
            return genres;
        }

        public async Task<BookResponse> GetBook(int bookId)
        {
            var bookResponse = await Client.GetAsync($"books/{bookId}");
            bookResponse.EnsureSuccessStatusCode();

            var stream = await bookResponse.Content.ReadAsStreamAsync();
            var book = await JsonSerializer.DeserializeAsync<BookResponse>
                (stream, new JsonSerializerOptions() { PropertyNameCaseInsensitive = true });
            return book;
        }

        public async Task<AuthorResponse> GetAuthor(int authorId)
        {
            var authorResponse = await Client.GetAsync($"authors/{authorId}");
            authorResponse.EnsureSuccessStatusCode();

            var stream = await authorResponse.Content.ReadAsStreamAsync();
            var author = await JsonSerializer.DeserializeAsync<AuthorResponse>
                (stream, new JsonSerializerOptions() { PropertyNameCaseInsensitive = true });
            
            return author;
        }

        public async Task<GenreResponse> GetGenre(int genreId)
        {
            var genreResponse = await Client.GetAsync($"genres/{genreId}");
            genreResponse.EnsureSuccessStatusCode();

            var stream = await genreResponse.Content.ReadAsStreamAsync();
            var genre = await JsonSerializer.DeserializeAsync<GenreResponse>
                (stream, new JsonSerializerOptions() { PropertyNameCaseInsensitive = true });

            return genre;
        }

    }
    
}
