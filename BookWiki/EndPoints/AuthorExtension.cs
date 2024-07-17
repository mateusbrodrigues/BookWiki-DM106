using BookWiki.Shared.Data.DB;
using BookWiki.Requests;
using BookWiki.Responses;
using BookWiki.Shared.Data.DB;
using BookWiki_Console;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.SqlServer.Query.Internal;


namespace BookWiki.EndPoints
{
    public static class AuthorExtension
    {
        public static void AddEndPointsAuthor(this WebApplication app)
        {

            app.MapGet("/Author", ([FromServices] DAL<Author> dal) =>
            {
                var authorList = dal.Read();
                if (authorList is null) return Results.NotFound();
                var authorResponseList = EntityListToResponseList(authorList);
                return Results.Ok(authorResponseList);
            });

            app.MapPost("/Author", ([FromServices] DAL<Author> dal, [FromBody] AuthorRequest authorRequest) =>
            {
                dal.Create(new Author(authorRequest.name));
                return Results.Ok();
            });

            app.MapDelete("/Author/{id}", ([FromServices] DAL<Author> dal, int id) =>
            {
                var author = dal.ReadBy(a => a.Id == id);
                if (author is null) return Results.NotFound();
                dal.Delete(author);
                return Results.NoContent();
            });

            app.MapPut("/Author", ([FromServices] DAL<Author> dal, [FromBody] AuthorEditRequest authorRequest) =>
            {
                var authorToEdit = dal.ReadBy(a => a.Id == authorRequest.id);
                if (authorToEdit is null) return Results.NotFound();
                authorToEdit.Name = authorRequest.name;
                authorToEdit.Nationality = authorRequest.Nationality;
                dal.Update(authorToEdit);
                return Results.Ok();
            });

            app.MapGet("/Author{id}", ([FromServices] DAL<Author> dal, int id) =>
            {
                var author = dal.ReadBy(a => a.Id == id);
                if (author is null) return Results.NotFound();
                return Results.Ok(EntityToResponse(author));
            });

        }

        private static ICollection<AuthorResponse> EntityListToResponseList(IEnumerable<Author> authorList)
        {
            return authorList.Select(a => EntityToResponse(a)).ToList();
        }
        private static AuthorResponse EntityToResponse(Author author)
        {
            return new AuthorResponse(
                author.Id,
                author.Name ?? string.Empty,
                author.Book?.Id ?? 0,
                author.Book?.Title ?? "No linked book");
        }
    }
}
