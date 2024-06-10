using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace _0410_Form기반
{
    public partial class Form5_User : Form
    {
        public Form5_User()
        {
            InitializeComponent();

            InitData();
        }

        private void InitData()
        {
            Array arr = System.Enum.GetValues(typeof(KnownColor));
            foreach(KnownColor color in arr)
            {
                listBox1.Items.Add(color.ToString());
                comboBox1.Items.Add(color.ToString());
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {

        }

        private void listBox1_DrawItem(object sender, DrawItemEventArgs e)
        {
            Graphics g = e.Graphics;

            Color c = Color.FromName(listBox1.Items[e.Index].ToString());
            Brush brush = new SolidBrush(c);
            //switch (e.Index)
            //{
            //    case 0: brush = new SolidBrush(c);      break;
            //    case 1: brush = new SolidBrush(c);      break;                 
            //    case 2: brush = new SolidBrush(c);      break;
            //        //brush = Brushes.Green;          break;
            //}
            //g.DrawRectangle(new Pen(c), e.Bounds);
            g.FillRectangle(brush, e.Bounds);
            g.DrawString(listBox1.Items[e.Index].ToString(),
                          e.Font, brush, e.Bounds, StringFormat.GenericDefault);
        }
    }
}
