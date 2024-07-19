using BookWiki.Shared.Data.DB;
using BookWiki.Requests;
using BookWiki.Responses;
using BookWiki_Console;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using BookWiki.Shared.Models;

namespace BookWiki.EndPoints
{
    public static class BookExtension
    {
        public static void AddEndPointsBook(this WebApplication app)
        {

            var groupBuilder = app.MapGroup("books")
                .RequireAuthorization()
                .WithTags("Books");

            groupBuilder.MapGet("", ([FromServices] DAL<Book> dal) =>
            {
                var bookList = dal.Read();
                if (bookList is null) return Results.NotFound();
                var bookResponseList = EntityListToResponseList(bookList);
                return Results.Ok(bookResponseList);
            });

            groupBuilder.MapPost("", ([FromServices] DAL<Book> dal, [FromServices] DAL<Genre> dalGenre,[FromBody] BookRequest bookRequest) =>
            {
                var book = new Book(bookRequest.title, bookRequest.summary, bookRequest.publicationYear)
                {
                    Genres = bookRequest.Genres is not null ?
                    GenreRequestConverter(bookRequest.Genres,dalGenre) : new List<Genre>()
                };
                dal.Create(book);
                return Results.Ok();
            });

            groupBuilder.MapDelete("/{id}", ([FromServices] DAL<Book> dal, int id) =>
            {
                var book = dal.ReadBy(a => a.Id == id);
                if (book is null) return Results.NotFound();
                dal.Delete(book);
                return Results.NoContent();
            });

            groupBuilder.MapPut("", ([FromServices] DAL<Book> dal, [FromBody] BookEditRequest bookRequest) =>
            {
                var bookToEdit = dal.ReadBy(a => a.Id == bookRequest.id);
                if (bookToEdit is null) return Results.NotFound();
                bookToEdit.Title = bookRequest.title;
                bookToEdit.Summary = bookRequest.summary;
                bookToEdit.PublicationYear = bookRequest.publicationYear;
                dal.Update(bookToEdit);
                return Results.Ok();
            });

            groupBuilder.MapGet("/{id}", ([FromServices] DAL<Book> dal, int id) =>
            {
                var book = dal.ReadBy(a => a.Id == id);
                if (book is null) return Results.NotFound();
                return Results.Ok(EntityToResponse(book));
            });

        }

        private static List<Genre> GenreRequestConverter(ICollection<GenreRequest> genres, DAL<Genre>dalGenre)
        {
            var genreList = new List<Genre>();
            foreach (var genre in genres)
            {
                var entity = RequestToEntity(genre);
                var l = dalGenre.ReadBy(a => a.Name.ToUpper().Equals(entity.Name.ToUpper()));
                if (l is not null) genreList.Add(l);
                else genreList.Add(entity);
                
            }
            return genreList;
        }

        private static Genre RequestToEntity(GenreRequest e)
        {
            return new Genre(e.Name);
        }

        private static ICollection<BookResponse> EntityListToResponseList(IEnumerable<Book> bookList)
        {
            return bookList.Select(a => EntityToResponse(a)).ToList();
        }

        private static BookResponse EntityToResponse(Book book)
        {
            return new BookResponse(book.Id, book.Title, book.Summary, book.PublicationYear);
        }

    }
}
