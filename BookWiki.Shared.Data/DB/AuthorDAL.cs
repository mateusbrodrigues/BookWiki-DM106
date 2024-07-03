using BookWiki.Shared.Data.DB;
using BookWiki_Console;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookWiki.Shared.Data.DB
{
    public class AuthorDAL
    {
        private readonly BookWikiContext context;

        public AuthorDAL(BookWikiContext context)
        {
            this.context = context;
        }

        public IEnumerable<Author> Read()
        {
            return context.Author.ToList();
        }

        public void Create(Author author)
        {
            context.Author.Add(author);
            context.SaveChanges();
        }

        public void Update(Author author)
        {
            context.Author.Update(author);
            context.SaveChanges();
        }

        public void Delete(Author author)
        {
            context.Author.Remove(author);
            context.SaveChanges();
        }

        public Author? ReadByName(string name)
        {
            return context.Author.FirstOrDefault(x => x.Name == name);
        }
    }
}