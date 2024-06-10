using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;

namespace _0410_WinForm
{
    internal partial class Form5 : Form
    {
        /// <summary>
        /// 디자이너에 의해 자동으로 생성
        /// </summary>
        public void Form_Init()
        {
            this.BackColor = Color.DarkOrchid;

            this.MouseMove += Form5_MouseMove;
        }
    }
}
