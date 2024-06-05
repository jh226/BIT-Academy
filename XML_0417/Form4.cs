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

namespace _0417_XML
{
    public partial class Form4 : Form
    {
        public Form4()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string msg = textBox1.Text;

            string json = NaverAPI.Example(msg);

            JObject jobject = JObject.Parse(json);

            textBox2.Text = jobject["message"]["result"]["translatedText"].ToString();
        }
    }
}
