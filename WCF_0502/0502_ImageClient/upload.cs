using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace _0502_ImageClient
{
    public partial class upload : Form
    {
        public string Name { get; set; }
        public string Share { get; set; }

        public upload()
        {
            InitializeComponent();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.OK;

            Name = textBox1.Text;
            if (checkBox1.Checked == true)
                Share = "true";
            else
                Share = "false";

            this.Close();
        }
    }
}
