using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace BookWiki_Console
{
    internal class Book
    {
        public Book(string title, string summary, int publicationYear)
        {
            Title = title;
            Summary = summary;
            PublicationYear = publicationYear;

        }

        public string Title { get; set; }
        public string Summary { get; set; }
        public int PublicationYear { get; set; }

        public List<Author> authors = new List<Author>();

        public void AddAuthor(Author author)
        {
            authors.Add(author);
        }

        public void ShowAuthors()
        {
            Console.WriteLine($"Autores de {Title}:");
            foreach (var author in authors)
            {
                Console.WriteLine(author);
            }
        }

        public override string ToString()
        {
            return $@"Nome do livro: {Title}";
        }

    }
}
