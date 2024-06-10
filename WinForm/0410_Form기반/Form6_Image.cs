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
    public partial class Form6_Image : Form
    {
        private Image _image = null;

        public Form6_Image()
        {
            InitializeComponent();
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);

            if(_image != null)
                e.Graphics.DrawImage(_image, 50, 50);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                _image = Image.FromFile("ocean.jpg");
                this.Invalidate();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + " 읽기 에러");
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                _image = Image.FromFile("ocean.jpg");

                //1. 이미지에 출력할 수 있는 그리기표면을 획득
                Graphics grfx = Graphics.FromImage(_image);

                Font font = new Font("돋음", 20);
                Brush brush = Brushes.Pink;

                //2. 이미지에 글자를 출력
                grfx.DrawString("이미지에 글자쓰기", font, brush, 10, 10);
                grfx.Dispose();

                //3. 변경된 이미지를 저장
                _image.Save("ocean.bmp", System.Drawing.Imaging.ImageFormat.Bmp);

                //4. 저장된 이미지 파일을 불러와 화면 출력
                this._image = Image.FromFile("ocean.bmp");
                this.Invalidate(this.ClientRectangle);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + " 읽기 에러");
            }
        }
    }
}
