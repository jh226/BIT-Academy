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
    public partial class Insert : Form
    {
        public string Mem_Name { get { return textBox1.Text; } }
        public int Mem_Phone { get { return int.Parse(textBox2.Text); } }
        public int Mem_Age { get { return int.Parse(textBox3.Text); } }


        public Insert()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e) //확인
        {
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void button2_Click(object sender, EventArgs e) //취소
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        private void Insert_Load(object sender, EventArgs e)
        {

        }
    }
}
