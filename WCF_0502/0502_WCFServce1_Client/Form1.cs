using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using _0502_WCFServce1_Client.ServiceReference1;


namespace _0502_WCFServce1_Client
{
    public partial class Form1 : Form
    {
        private HelloWorldClient c = new HelloWorldClient();

        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            textBox1.Text = c.SayHello();
        }

        //등록
        private void button2_Click(object sender, EventArgs e)
        {
            int num = int.Parse(textBox2.Text);
            string name = textBox3.Text; 

            if( c.Insert_Product(num , name) == false)
            {
                MessageBox.Show("에러");
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            int count = c.Product_Count();
            this.Text = count.ToString();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            listBox1.Items.Clear();
            Product[] products =  c.GetList_Product();

            foreach(Product product in products)
            {
                listBox1.Items.Add(string.Format(product.ProductId + ", " +
                            product.ProductName));
            }
        }
    }
}
