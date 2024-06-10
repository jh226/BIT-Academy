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
    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();
        }

        private void Form2_Load(object sender, EventArgs e)
        {
            Form[] newMDIChild = new Form[3];  
            for (int i = 0; i < 3; i++)
            {
                newMDIChild[i] = new Form();
                newMDIChild[i].Text = i + "번째 자식창";
                newMDIChild[i].MdiParent = this;  // ❸. MDI 자식 폼 만들기
                newMDIChild[i].Closed += Form2_Closed;
                newMDIChild[i].Show();
            }
        }

        private void Form2_Closed(object sender, EventArgs e)
        {
            //throw new NotImplementedException();
        }

        private void Form2_FormClosed(object sender, FormClosedEventArgs e)
        {

        }

        #region 버튼 클릭 이벤트 
        private void button1_Click(object sender, EventArgs e)
        {
            if( sender == this.button1)
            {
                this.LayoutMdi(MdiLayout.TileHorizontal);
                this.Text = "수평 바둑판 정렬";
            }
            else if(sender == this.button2)
            {
                this.LayoutMdi(MdiLayout.TileVertical);
                this.Text = "수직 바둑판 정렬";
            }
            else if( sender == this.button3)
            {
                this.LayoutMdi(MdiLayout.Cascade);
                this.Text = "계단식 정렬";

            }
            else if(sender==this.button4)
            {
                this.LayoutMdi(MdiLayout.ArrangeIcons);
                this.Text = "아이콘 정렬";
            }
        }
        #endregion
    }
}
