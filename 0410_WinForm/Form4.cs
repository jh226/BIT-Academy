using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace _0410_WinForm
{
    internal class Form4 : Form
    {
        public Form4() // 1. 속성 초기화 + 2. 자식 컨트롤 객체 생성 + 3. Event 등록처리
        {
            #region 1.속성 초기화
            this.Top = 10;
            this.Left = 10;
            this.Width = 250;
            this.Height = 200;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            #endregion

            #region 3. Event 등록
            this.Load += Form4_Load;
            this.FormClosed += Form4_FormClosed;
            this.FormClosing += Form4_FormClosing;

            this.MouseEnter += Form4_MouseEnter;
            this.MouseLeave += Form4_MouseLeave;
            this.MouseMove += Form4_MouseMove;
            #endregion

            this.Show();
        }

        private void Form4_FormClosing(object sender, FormClosingEventArgs e)
        {
            DialogResult r = MessageBox.Show("종료하시겠습니까?","알림",
                MessageBoxButtons.OKCancel);

            if(r ==DialogResult.Cancel)
                e.Cancel = true;
        }

        private void Form4_MouseMove(object sender, MouseEventArgs e)
        {
            //this.Text = e.Location.ToString();
            this.Text = string.Format("{0}, {1}", e.X, e.Y);
        }

        private void Form4_MouseLeave(object sender, EventArgs e)
        {
            this.Text = "MouseLeave";
        }

        private void Form4_MouseEnter(object sender, EventArgs e)
        {
            this.Text = "MouseEnter";
        }

        private void Form4_FormClosed(object sender, FormClosedEventArgs e)
        {
            this.Text = "FormClosed";            
        }

        private void Form4_Load(object sender, EventArgs e)
        {
            this.Text = "FormLoad";
        }
    }
}
