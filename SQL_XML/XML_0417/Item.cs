using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace _0417_XML
{
    internal class Item
    {
        public string Title { get; set; }
        public string Link { get; set; }
        public string Description { get; set; }
        public DateTime Date { get; set; }
        public string Author { get; set; }
        public string Category { get; set; }

        public Item(string title, string link, string description, DateTime date, string author, string category)
        {
            Title           = title;
            Link            = link;
            Description     = description;
            Date            = date;
            Author          = author;
            Category        = category;
        }

        internal static Item MakeItem(XmlReader xr)
        {
            xr.ReadToDescendant("title");
            string title = xr.ReadElementString("title");

            xr.ReadToNextSibling("link");
            string price = xr.ReadElementString("link");

            xr.ReadToNextSibling("description");
            string description = xr.ReadElementString("description");

            //xr.ReadToNextSibling("date");
            //DateTime date = DateTime.Parse(xr.ReadElementString("date"));
            DateTime date = DateTime.Now;

            xr.ReadToNextSibling("author");
            string author = xr.ReadElementString("author");

            xr.ReadToNextSibling("category");
            string category = xr.ReadElementString("category");

            return new Item(title, price, description, date,
                                                author, category);
        }
    }
}
