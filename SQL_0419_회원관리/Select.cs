using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace _0418_DB
{
    public partial class Select : Form
    {
        public string Mem_Name { get { return textBox1.Text; } }
        public Form1 Parent_From { get; set; } = null;


        public Select()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Parent_From.select_acc();
        }

        private void Select_FormClosed(object sender, FormClosedEventArgs e)
        {
            Parent_From.Select = null;
        }
    }
}
