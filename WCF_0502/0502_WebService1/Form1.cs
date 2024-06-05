using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using _0502_WebService1.WebReference;

namespace _0502_WebService1
{
    public partial class Form1 : Form
    {
        //proxy객체 
        private ShowHelloService service = new ShowHelloService();

        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //textBox1.Text = service.ShowHello();

            int value = service.Add(10, 20);
            textBox1.Text = value.ToString();
        }
    }
}
