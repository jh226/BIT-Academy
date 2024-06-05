using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace _0410_WinForm
{
    internal partial class Form5 : Form
    {
        public Form5()
        {
            Form_Init();
        }

        private void Form5_MouseMove(object sender, MouseEventArgs e)
        {
            this.Text = e.Location.ToString();
        }
    }
}
