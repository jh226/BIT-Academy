using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace _0417_XML
{
    internal class Book
    {
        internal string Title { get; private set; }
        internal int Price { get; private set; }

        public Book(string title, int price)
        {
            Title = title;
            Price = price;
        }

        public override string ToString()
        {
            return Title + "\t" + Price;
        }

        internal static Book MakeBook(XmlReader xr)
        {
            xr.ReadToDescendant("title");
            string title = xr.ReadElementString("title");

            xr.ReadToNextSibling("가격");
            int price = int.Parse(xr.ReadElementString("가격"));            

            return new Book(title, price);
        }
    }
}
