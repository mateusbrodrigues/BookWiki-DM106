using BookWiki.Shared.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace BookWiki_Console
{
    public class Book
    {
        public int Id { get; set; }
        public Book(string title, string summary, int publicationYear)
        {
            Title = title;
            Summary = summary;
            PublicationYear = publicationYear;

        }

        public string Title { get; set; }
        public string Summary { get; set; }
        public int PublicationYear { get; set; }

        public virtual ICollection<Author> Authors { get; set; } = new List<Author>();
        public virtual ICollection<Genre> Genres { get; set; }
        public void AddAuthor(Author author)
        {
            //Authors.Add(author);
            Authors.Append(author);
        }

        public void ShowAuthors()
        {
            Console.WriteLine($"Autores de {Title}:");
            foreach (var author in Authors)
            {
                Console.WriteLine(author);
            }
        }

        public override string ToString()
        {
            return $@"Id: {Id} | Nome do livro: {Title}";
        }

    }
}
