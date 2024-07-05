using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace BookWiki_Console
{
    public class Author
    {
        public int Id { get; set; }
        public Author(string name)
        {
            Name = name;
        }

        public string Name { get; set; }
        public string Nationality { get; set; }

        public virtual Book? Book { get; set; }

        public override string ToString()
        {
            return $@"Id: {Id} | Author: {Name}";
        }
    }
}
