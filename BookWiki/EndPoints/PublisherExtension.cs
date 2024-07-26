using BookWiki.Shared.Data.DB;
using BookWiki.Requests;
using BookWiki.Responses;
using BookWiki.Shared.Data.DB;
using BookWiki_Console;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.SqlServer.Query.Internal;


namespace BookWiki.EndPoints
{
    public static class PublisherExtension
    {
        public static void AddEndPointsPublisher(this WebApplication app)
        {

            var groupBuilder = app.MapGroup("publishers")
                    .RequireAuthorization()
                    .WithTags("Publishers");

            groupBuilder.MapGet("", ([FromServices] DAL<Publisher> dal) =>
            {
                var publisherList = dal.Read();
                if (publisherList is null) return Results.NotFound();
                var publisherResponseList = EntityListToResponseList(publisherList);
                return Results.Ok(publisherResponseList);
            });

            groupBuilder.MapPost("", ([FromServices] DAL<Publisher> dal, [FromBody] PublisherRequest publisherRequest) =>
            {
                dal.Create(new Publisher(publisherRequest.name, publisherRequest.location));
                return Results.Ok();
            });

            groupBuilder.MapDelete("/{id}", ([FromServices] DAL<Publisher> dal, int id) =>
            {
                var publisher = dal.ReadBy(p => p.Id == id);
                if (publisher is null) return Results.NotFound();
                dal.Delete(publisher);
                return Results.NoContent();
            });

            groupBuilder.MapPut("", ([FromServices] DAL<Publisher> dal, [FromBody] PublisherEditRequest publisherRequest) =>
            {
                var publisherToEdit = dal.ReadBy(p => p.Id == publisherRequest.id);
                if (publisherToEdit is null) return Results.NotFound();
                publisherToEdit.Name = publisherRequest.name;
                publisherToEdit.Location = publisherRequest.location;
                dal.Update(publisherToEdit);
                return Results.Ok();
            });

            groupBuilder.MapGet("/{id}", ([FromServices] DAL<Publisher> dal, int id) =>
            {
                var publisher = dal.ReadBy(p => p.Id == id);
                if (publisher is null) return Results.NotFound();
                return Results.Ok(EntityToResponse(publisher));
            });

        }

        private static ICollection<PublisherResponse> EntityListToResponseList(IEnumerable<Publisher> publisherList)
        {
            return publisherList.Select(p => EntityToResponse(p)).ToList();
        }

        private static PublisherResponse EntityToResponse(Publisher publisher)
        {
            return new PublisherResponse(
                publisher.Id,
                publisher.Name,
                publisher.Location,
                 publisher.Book?.Id ?? 0,
                publisher.Book?.Title ?? "No linked book");
        }
    }

 


}
