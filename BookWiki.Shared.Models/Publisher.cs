using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace BookWiki_Console
{
    public class Publisher
    {
        public Publisher(string name, string location)
        {
            Name = name;
            Location = location;
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string Location { get; set; }

        public virtual Book? Book { get; set; }

        public override string ToString()
        {
            return $@"Id: {Id} | Publisher: {Name}, Location: {Location}";
        }
    }

}
