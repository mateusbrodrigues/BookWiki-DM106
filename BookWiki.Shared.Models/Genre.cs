using BookWiki_Console;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookWiki.Shared.Models
{
    public class Genre
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public Genre(string name)
        {
            Name = name;
        }

        public  virtual ICollection<Book> Books { get; set; }
        public override string ToString()
        {
            return $@"Id: {Id} - Name: {Name}";
        }
    }
}
