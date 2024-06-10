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
    public partial class Form_Insert : Form
    {
        public string Member_Name   { get { return textBox1.Text;       }    }
        public string Member_Phone  { get { return textBox2.Text;       }    }
        public int Member_Age       { get { return int.Parse(textBox3.Text); } }

        public Form_Insert()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.OK;
            this.Close();
        }
    }
}
