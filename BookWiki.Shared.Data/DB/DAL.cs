using BookWiki_Console;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookWiki.Shared.Data.DB
{
    public class DAL<T> where T : class
    {
        private readonly BookWikiContext context;

        public DAL(BookWikiContext context)
        {
            this.context = context;
        }
        public IEnumerable<T> Read()
        {
            return context.Set<T>().ToList();
        }

        public void Create(T entity)
        {
            context.Set<T>().Add(entity);
            context.SaveChanges();
        }

        public void Update(T entity)
        {
            context.Set<T>().Update(entity);
            context.SaveChanges();
        }

        public void Delete(T entity)
        {
            context.Set<T>().Remove(entity);
            context.SaveChanges();
        }

        public T? ReadBy(Func<T,bool> predicate)//title
        {
            return context.Set<T>().FirstOrDefault(predicate);

        }
    }


}
