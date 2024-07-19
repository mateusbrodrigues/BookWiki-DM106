namespace BookWiki.Requests
{
    public record BookRequest(string title, string summary, int publicationYear, ICollection<GenreRequest> Genres = null);
}
