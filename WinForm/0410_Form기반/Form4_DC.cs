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
    public partial class Form4_DC : Form
    {
        public Form4_DC()
        {
            InitializeComponent();
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);

            Graphics g = pictureBox3.CreateGraphics();
            g.FillRectangle(new SolidBrush(Color.Gold), pictureBox3.ClientRectangle);
            g.Dispose();

            button1_Click(this, new EventArgs());
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //Graphics g = pictureBox1.CreateGraphics();
            //g.FillRectangle(new SolidBrush(Color.Blue), pictureBox1.ClientRectangle);
            //g.Dispose();

            using(Graphics g2 = pictureBox2.CreateGraphics())
            {
                g2.FillRectangle(new SolidBrush(Color.DarkTurquoise), pictureBox2.ClientRectangle);
            }
        }

        private void pictureBox1_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            g.FillRectangle(new SolidBrush(Color.Green), pictureBox1.ClientRectangle);
        }
    }
}
