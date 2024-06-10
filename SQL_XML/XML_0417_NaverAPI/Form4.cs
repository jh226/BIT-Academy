using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace _0417_XML
{
    public partial class Form4 : Form
    {
        private List<Item> items = null;
        JObject jobject;
        public Form4()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            items = new List<Item>();
            string msg = textBox1.Text;

            string json = NaverAPI.Example(msg);

            jobject = JObject.Parse(json);

            textBox2.Text = jobject["items"].ToString();

            //-----------------------------------------------------------------------------
            for (int i = 0; i < jobject["items"].Count(); i++)
            {
                items.Add(Item.MakeItem(jobject, i));

                comboBox1.Items.Add(items[i].Title);
            }
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            int idx = comboBox1.SelectedIndex;
            this.Text = items[idx].Title;
            textBox2.Text = items[idx].Description;
        }

    }
}
