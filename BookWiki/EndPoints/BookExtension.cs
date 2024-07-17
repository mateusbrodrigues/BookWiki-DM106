using BookWiki.Shared.Data.DB;
using BookWiki.Requests;
using BookWiki.Responses;
using BookWiki_Console;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;


namespace BookWiki.EndPoints
{
    public static class BookExtension
    {
        public static void AddEndPointsBook(this WebApplication app)
        {
            app.MapGet("/Books", ([FromServices] DAL<Book> dal) =>
            {
                var bookList = dal.Read();
                if (bookList is null) return Results.NotFound();
                var bookResponseList = EntityListToResponseList(bookList);
                return Results.Ok(bookResponseList);
            });

            app.MapPost("/Books", ([FromServices] DAL<Book> dal, [FromBody] BookRequest bookRequest) =>
            {
                dal.Create(new Book(bookRequest.title, bookRequest.summary,bookRequest.publicationYear));
                return Results.Ok();
            });

            app.MapDelete("/Books/{id}", ([FromServices] DAL<Book> dal, int id) =>
            {
                var book = dal.ReadBy(a => a.Id == id);
                if (book is null) return Results.NotFound();
                dal.Delete(book);
                return Results.NoContent();
            });

            app.MapPut("/Books", ([FromServices] DAL<Book> dal, [FromBody] BookEditRequest bookRequest) =>
            {
                var bookToEdit = dal.ReadBy(a => a.Id == bookRequest.id);
                if (bookToEdit is null) return Results.NotFound();
                bookToEdit.Title = bookRequest.title;
                bookToEdit.Summary = bookRequest.summary;
                bookToEdit.PublicationYear = bookRequest.publicationYear;
                dal.Update(bookToEdit);
                return Results.Ok();
            });

            app.MapGet("/Books{id}", ([FromServices] DAL<Book> dal, int id) =>
            {
                var book = dal.ReadBy(a => a.Id == id);
                if (book is null) return Results.NotFound();
                return Results.Ok(EntityToResponse(book));
            });

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
