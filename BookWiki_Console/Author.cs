﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace BookWiki_Console
{
    internal class Author
    {
        public Author(string name)
        {
            Name = name;
        }

        public string Name { get; set; }
        public string Nationality { get; set; }

        public override string ToString()
        {
            return $@"Author: {Name}";
        }
    }
}
