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
    public partial class Form3_Child : Form
    {
        public string StrText { get; set; }

        public Form3_Child(string str)
        {
            InitializeComponent();

            this.Text = str;

            SetStyle(ControlStyles.SupportsTransparentBackColor, true);
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            Graphics grfx = e.Graphics;
            SolidBrush br = new SolidBrush(Color.Black);
            grfx.DrawString(this.StrText, this.Font, br, 10, 7);            

            //base.OnPaint(e);
        }
    }
}
