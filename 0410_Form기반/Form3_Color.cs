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
    public partial class Form3_Color : Form
    {
        public Form3_Color()
        {
            InitializeComponent();

            Array arr = System.Enum.GetValues(typeof(KnownColor));
            Form3_Child[] frm = new Form3_Child[arr.Length];

            for (int i = 0; i < arr.Length; i++)
            {
                frm[i] = new Form3_Child(arr.GetValue(i).ToString());
                frm[i].StrText = arr.GetValue(i).ToString();
                frm[i].BackColor = Color.FromName(arr.GetValue(i).ToString());
                frm[i].SetBounds(0, 0, 200, 50);
                frm[i].MdiParent = this;
                frm[i].Show();
            }
        }

        private void 프로그램종료XToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close(); //SendMessage(WM_CLOSE)
        }

        private void 정렬SToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.LayoutMdi(MdiLayout.TileVertical);
        }
    }
}
