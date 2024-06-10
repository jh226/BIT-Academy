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
using _0502_ImageClient.WebReference;

namespace _0502_ImageClient
{
    public partial class Form1 : Form
    {
        private WbDB db = WbDB.Instance;  //프로시저 기반


        //웹 서비스 객체는 메서드 호출시 마다 생성과 소멸을 반복한다.
        //상태가 없는 와이어프로토콜구조이다.
        private WbService wbService = new WbService();
        private Image picImage      = null;

        public Form1()
        {
            InitializeComponent();
        }

        #region Form Load & Closed(DB연결 및 해제 처리)

        private void Form1_Load(object sender, EventArgs e)
        {
            if (db.Open() == true)
            {
                this.Text = "데이터 베이스에 연결되었습니다.....";
                //PrintAll();
            }
        }

        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (db.Close() == true)
            {
                this.Text = "데이터 베이스 연결이 종료되었습니다....";
            }
        }
        #endregion

        private void 그림목록보기ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            PicListForm plf = new PicListForm();
            
            if (plf.ShowDialog(this) == DialogResult.OK)
            {
                byte[] filedata = wbService.GetPicture(plf.SelectedPic);
                picImage = Image.FromStream(new MemoryStream(filedata));

                // 이미지 크기와 창크기를 맞춘다.
                this.ClientSize = picImage.Size; 
                this.Text = "<파일명 : " + plf.SelectedPic + "> PictureService에서 제공받은 그림파일을 보여주는 클라이언트";
                Invalidate();   // 화면을 갱신한다.
            }
        }

        private void 그림업로드하기ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // 읽어오는 스트림클래스를 선언
            Stream readStream;

            // 파일열기	대화상자를 생성
            OpenFileDialog dlg = new OpenFileDialog();

            // 확장자를 제한한다.
            dlg.Filter = "그림파일 (*.bmp;*.jpg;*.gif;*.jpeg;*.png;*.tiff)|*.bmp;*.jpg;*.gif;*.jpeg;*.png;*.tiff)";
            dlg.RestoreDirectory = true;    // 현재 디렉토리를 저장해놓는다.

            // OK 버튼을 누르면
            if (dlg.ShowDialog() == DialogResult.OK)
            {
                if ((readStream = dlg.OpenFile()) != null)
                {
                    byte[] bytePic;
                    BinaryReader picReader = new BinaryReader(readStream);
                    bytePic = picReader.ReadBytes(Convert.ToInt32(readStream.Length));
                    FileInfo fi = new FileInfo(dlg.FileName);



                    // 업로드 서비스 요청
                    if (wbService.UploadPicture(fi.Name, bytePic))
                    {
                        MessageBox.Show("업로드 성공");
                    }
                    else
                    {
                        MessageBox.Show("업로드 실패");
                    }
                    readStream.Close();

                    upload load = new upload();
                    if(load.ShowDialog() == DialogResult.OK)
                        db.InsertImage(fi.Name, load.Name, load.Share, bytePic);
                }
            }
        }

        private void 프로그램종료ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            //base.OnPaint(e);
            if (picImage == null)
            {
                return;
            }

            Graphics g = e.Graphics;
            g.DrawImage(picImage, ClientRectangle);
        }

        

    }
}
