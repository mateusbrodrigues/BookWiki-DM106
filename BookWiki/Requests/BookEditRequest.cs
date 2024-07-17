namespace BookWiki.Requests
{
    public record BookEditRequest(int id, string title, string summary, int publicationYear);
}
