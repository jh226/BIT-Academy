using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace _0418_DB
{
    public partial class Form_Select : Form
    {
        public string Member_Name { get { return textBox1.Text; } }

        public MainForm Main_Form { get; set; } = null;

        public Form_Select()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Main_Form.Apply();
        }

        private void Form_Select_FormClosed(object sender, FormClosedEventArgs e)
        {
            Main_Form.SelectForm = null;
        }
    }
}
