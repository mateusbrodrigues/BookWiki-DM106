using BookWiki_Console;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookWiki.Shared.Data.DB
{
    public class BookDAL
    {   
        private readonly BookWikiContext context;

        public BookDAL(BookWikiContext context)
        {
            this.context = context;
        }

        public IEnumerable<Book> Read()
        {
            return context.Book.ToList();
        }

        public void create(Book book)
        {
            context.Book.Add(book);
            context.SaveChanges();
        }

        public void Update(Book book)
        {
            context.Book.Update(book);
            context.SaveChanges();
        }

        public void Delete(Book book)
        {
            context.Book.Remove(book);
            context.SaveChanges();
        }

        public Book? ReadByName(string title)
        {
            return context.Book.FirstOrDefault(x => x.Title == title);

        }
    }


    
}
