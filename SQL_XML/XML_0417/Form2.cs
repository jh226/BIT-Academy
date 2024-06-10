using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;

namespace _0417_XML
{
    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();
        }

        //1. XML문서읽기
        private void button1_Click(object sender, EventArgs e)
        {
            //data0.xml, data.xml, data1.xml(특성)
            textBox1.Text = WbXml.Read4("data1.xml");

            textBox2.Text = WbXml.Parse1("data1.xml");
        }

        //2. 파싱
        private void button2_Click(object sender, EventArgs e)
        {
            List<Book> ar = new List<Book>();

            XmlReader reader = XmlReader.Create("data1.xml");
            reader.MoveToContent();
            while (reader.Read())
            {
                if (reader.IsStartElement("book"))
                {
                    Book book = Book.MakeBook(reader);
                    if (book != null) { ar.Add(book); }
                }
            }

            string str = string.Empty;
            str += "도서 개수:" +  ar.Count + "\r\n";
            foreach (Book book in ar)
            {
                str += book + "\r\n";
            }
            textBox3.Text = str; 
        }
    }
}
