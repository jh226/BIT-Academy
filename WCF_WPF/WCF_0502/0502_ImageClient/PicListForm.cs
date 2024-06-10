using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using _0502_ImageClient.WebReference;

namespace _0502_ImageClient
{
    public partial class PicListForm : Form
    {
        private WbService wbService = new WbService();

        public string SelectedPic
        {
            get{   return listBox1.SelectedItem.ToString(); }
        }

        public PicListForm()
        {
            InitializeComponent();

            // 이미지 파일의 목록을 가져오는 메소드를 호출해서 문자열 배열에 저장한다.
            string[] strPicList = wbService.GetPictureList();
            listBox1.DataSource = strPicList;
        }

        private void listBox1_DoubleClick(object sender, EventArgs e)
        {
            DialogResult = DialogResult.OK;
            this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.OK;
            this.Close();
        }
    }
}
