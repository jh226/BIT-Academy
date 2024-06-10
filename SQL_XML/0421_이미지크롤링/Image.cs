using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using System.Xml.Linq;

namespace _0421
{
    internal class Image
    {         
        public string Title { get; set; }
        public byte[] Data { get; set; }
        public int Size { get; set; }

        public Image(string title, byte[] data, int size)
        {
            Title = title;
            Data = data;
            Size = size;
        }

        public override string ToString()
        {
            return Title + ", " + Data + ", " + Size;
        }
    }
}
