using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace _0417_XML
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        //XmlWriter1
        private void button1_Click(object sender, EventArgs e)
        {
            // WbXml.WriteTest1();
            //textBox1.Text = WbXml.Write3();
            textBox1.Text = WbXml.Read1("data1.xml");
        }

        //XmlWriter2
        private void button2_Click(object sender, EventArgs e)
        {
            //textBox1.Text =  WbXml.WriteTest2();
            textBox1.Text = WbXml.Read2("data1.xml");
        }

        //RSS문서
        private void button3_Click(object sender, EventArgs e)
        {
            textBox2.Text = WbXml.UrlReader("http://www.khan.co.kr/rss/rssdata/total_news.xml");
        }
    }
}
