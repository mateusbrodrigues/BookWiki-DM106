using BookWiki.Requests;
using BookWiki.Responses;
using BookWiki.Shared.Data.DB;
using BookWiki.Shared.Models;
using Microsoft.AspNetCore.Mvc;

namespace BookWiki.EndPoints
{
    public static class GenreExtension
    {
        public static void AddEndPointsGenre(this WebApplication app)
        {
            var groupBuilder = app.MapGroup("genres")
                   .RequireAuthorization()
                   .WithTags("Genres");

            groupBuilder.MapGet("", ([FromServices] DAL<Genre> dal) =>
            {
                var genreList = dal.Read();
                if (genreList is null) return Results.NotFound();
                var genreResponseList = EntityListToResponseList(genreList);
                return Results.Ok(genreResponseList);
            });
            groupBuilder.MapPost("", ([FromServices] DAL<Genre> dal, [FromBody] GenreRequest genreRequest) =>
            {
                dal.Create(RequestToEntity(genreRequest));
                return Results.Ok();
            });
            groupBuilder.MapDelete("/{id}", ([FromServices] DAL<Genre> dal, int id) =>
            {
                var genre = dal.ReadBy(a => a.Id == id);
                if (genre is null) return Results.NotFound();
                dal.Delete(genre);
                return Results.NoContent();
            });
        }

        private static Genre RequestToEntity(GenreRequest genreRequest)
        {
            return new Genre(genreRequest.Name);
        }
        private static ICollection<GenreResponse> EntityListToResponseList(IEnumerable<Genre> genreList)
        {
            return genreList.Select(e => EntityToResponse(e)).ToList();
        }
        private static GenreResponse EntityToResponse(Genre e)
       {
            return new GenreResponse(e.Id, e.Name);
        }
    }
}
