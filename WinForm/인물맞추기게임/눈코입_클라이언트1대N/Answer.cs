using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace 눈코입_클라이언트
{
    public partial class Answer : Form
    {
        byte[] imagedata;
        string answer;
        public Answer(byte[] data, string answer)
        {
            InitializeComponent();
            this.imagedata = data;
            this.answer = answer;
        }

        private void Answer_Load(object sender, EventArgs e)
        {
            // 바이트 배열에서 이미지 로드
            using (MemoryStream ms = new MemoryStream(imagedata))
            {
                Image image = Image.FromStream(ms);
                pictureBox1.Image = image;
                textBox1.Text = answer;
            }
        }
    }
}
