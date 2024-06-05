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
    public partial class Form3 : Form
    {
        private List<Item> items = null;

        public Form3()
        {
            InitializeComponent();
        }

        private void Form3_Load(object sender, EventArgs e)
        {
            button1_Click(sender, e);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            
            items = WbXml.ItemParse("http://www.khan.co.kr/rss/rssdata/total_news.xml");

            foreach (Item item in items)
            {
                comboBox1.Items.Add(item.Title);
            }
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            int idx = comboBox1.SelectedIndex;
            this.Text = items[idx].Link;

            webBrowser1.Navigate(this.Text);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            pictureBox1.ImageLocation = "https://cdn.pixabay.com/photo/2016/05/07/09/40/social-media-1377251_960_720.png";
        }
    }
}
