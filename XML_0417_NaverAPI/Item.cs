using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace _0417_XML
{
    internal class Item
    {
        public static List<Item> items = new List<Item>();
        public string Title { get; set; }
        public string Link { get; set; }
        public string Description { get; set; }
        public string Image_url { get; set; }

        public Item(string title, string link, string description, string url)
        {
            Title = title;
            Link = link;
            Description = description;
            Image_url = url;
        }

        internal static Item MakeItem(JObject jobject, int num)
        {

            string title = jobject["items"][num]["title"].ToString();

            string link = jobject["items"][num]["link"].ToString();

            string description = jobject["items"][num]["description"].ToString();

            string image_url = jobject["items"][num]["link"].ToString();

            return new Item(title, link, description, image_url);
        }
    }
}
