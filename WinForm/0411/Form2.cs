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

namespace _0411
{
    public partial class Form2 : Form
    {
        #region 전역 변수

        bool flag;
        byte[,] inImage, outImage;
        double[,] intempImage, outtempImage;

        int inH = 0, inW = 0, outH = 0, outW = 0;

        

        String fileName;
        Bitmap bitmap;

        #endregion

        public Form2()
        {
            InitializeComponent();
        }

        #region 기능
        public void openImage()
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.ShowDialog();
            fileName = ofd.FileName;
            // 파일의 크기를 알아내기
            long fSize = new FileInfo(fileName).Length;
            // 중요! 입력메모리의 크기를 알아냄.
            inH = inW = (int)Math.Sqrt((double)fSize);
            // 입력 메모리 확보
            inImage = new byte[inH, inW];
            // 파일 --> 입력 메모리 로딩
            BinaryReader br = new BinaryReader(File.Open(fileName, FileMode.Open));
            for (int i = 0; i < inH; i++)
                for (int k = 0; k < inW; k++)
                    inImage[i, k] = br.ReadByte();
            br.Close();
            // MessageBox.Show(inImage[100, 100].ToString());
            //displayImage();

            equalImage();
        }

        void displayImage()
        {
            // 비트맵 및 픽처박스 크기 조절
            bitmap = new Bitmap(outH, outW);
            pictureBox1.Size = new Size(outH, outW);
            this.Size = new Size(outH + 20, outW + 90);

            Color pen;// 비트맵에 콕콕 찍을 펜.
            for (int i = 0; i < outH; i++)
                for (int k = 0; k < outW; k++)
                {

                    byte data = outImage[i, k]; // 한점(=색상)
                    pen = Color.FromArgb(data, data, data); //펜에 잉크 묻힘
                    bitmap.SetPixel(k, i, pen); // 콕! 점찍기.
                }

            pictureBox1.Image = bitmap;
            toolStripStatusLabel1.Text = "크기:" + outH + "x" + outW;
        }

        //영상처리함수
        void equalImage()
        {
            // 중요! 출력 이미지의 크기. --> 알고리즘에 의존.
            outH = inH;
            outW = inW;
            // 출력 이미지 메모리 할당
            outImage = new byte[outH, outW];
            /// 진짜! 영상처리 알고리즘을 구현
            for (int i = 0; i < inH; i++)
                for (int k = 0; k < inW; k++)
                {
                    outImage[i, k] = inImage[i, k];
                }
            displayImage();
            //MessageBox.Show(outImage[100, 100].ToString());
        }

        void darkImage()
        {
            // 중요! 출력 이미지의 크기. --> 알고리즘에 의존.
            outH = inH;
            outW = inW;
            // 출력 이미지 메모리 할당
            outImage = new byte[outH, outW];
            /// 진짜! 영상처리 알고리즘을 구현
            /// 
            SubForm sub = new SubForm();
            if (sub.ShowDialog() == DialogResult.Cancel)
                return;
            int value = (int)sub.numericUpDown1.Value;
            
            for (int i = 0; i < inH; i++)
                for (int k = 0; k < inW; k++)
                {
                    int pixel = inImage[i, k];
                    if (pixel < value)
                        pixel = 0;
                    else if (pixel > value)
                        pixel = 255;

                    outImage[i, k] = (byte)pixel;
                }

            displayImage();
            //MessageBox.Show(outImage[100, 100].ToString());
        }

        void reverseImage()
        {
            // 중요! 출력 이미지의 크기. --> 알고리즘에 의존.
            outH = inH;
            outW = inW;
            // 출력 이미지 메모리 할당
            outImage = new byte[outH, outW];
            /// 진짜! 영상처리 알고리즘을 구현
            for (int i = 0; i < inH; i++)
                for (int k = 0; k < inW; k++)
                {
                    outImage[i, k] = (byte)(255 - inImage[i, k]);
                }
            displayImage();
            //MessageBox.Show(outImage[100, 100].ToString());
        }

        void addImage()
        {

            // 중요! 출력 이미지의 크기. --> 알고리즘에 의존.
            outH = inH;
            outW = inW;
            // 출력 이미지 메모리 할당
            outImage = new byte[outH, outW];
            /// 진짜! 영상처리 알고리즘을 구현
            //int value = 100;
            SubForm sub = new SubForm();
            if (sub.ShowDialog() == DialogResult.Cancel)
                return;
            int value = (int)sub.numericUpDown1.Value;

            for (int i = 0; i < inH; i++)
                for (int k = 0; k < inW; k++)
                {
                    if ((inImage[i, k] + value) >= 255)
                        outImage[i, k] = 255;
                    else if ((inImage[i, k] + value) <= 0)
                        outImage[i, k] = 0;
                    else outImage[i, k] = (byte)(inImage[i, k] + value);

                    //outImage[i, k] = (byte)pixel;
                }
            displayImage();
            //MessageBox.Show(outImage[100, 100].ToString());
        }
        void zoomOutImage()
        {
            int scale = 2; //2배 축소
            // 중요! 출력 이미지의 크기. --> 알고리즘에 의존.
            outH = inH / scale;
            outW = inW / scale;
            // 출력 이미지 메모리 할당
            outImage = new byte[outH, outW];
            /// 진짜! 영상처리 알고리즘을 구현
            for (int i = 0; i < inH; i++)
                for (int k = 0; k < inW; k++)
                {
                    outImage[i / scale, k / scale] = inImage[i, k];
                }
            displayImage();
            //MessageBox.Show(outImage[100, 100].ToString());
        }

        void zoomInImage()
        {
            int scale = 2; //2배 축소
            // 중요! 출력 이미지의 크기. --> 알고리즘에 의존.
            outH = inH * scale;
            outW = inW * scale;
            // 출력 이미지 메모리 할당
            outImage = new byte[outH, outW];
            /// 진짜! 영상처리 알고리즘을 구현
            for (int i = 0; i < outH; i++)
                for (int k = 0; k < outW; k++)
                {
                    outImage[i, k] = inImage[i / scale, k / scale];
                }
            displayImage();
            //MessageBox.Show(outImage[100, 100].ToString());
        }

        void saveImage()
        {
            SaveFileDialog sfd = new SaveFileDialog();
            sfd.DefaultExt = "raw";
            sfd.Filter = "로우 이미지(*.raw) | *.raw";
            if (sfd.ShowDialog() == DialogResult.OK)
            {
                string saveFname = sfd.FileName;
                BinaryWriter bw = new BinaryWriter(File.Open(saveFname, FileMode.Create));
                for (int i = 0; i < outH; i++)
                    for (int k = 0; k < outW; k++)
                        bw.Write(outImage[i, k]);
                bw.Close();
                // 메시지 박스를 보이면 좋음.
            }
        }

        void leftrightMirror()
        {
            // 중요! 출력 이미지의 크기. --> 알고리즘에 의존.
            outH = inH;
            outW = inW;
            // 출력 이미지 메모리 할당
            outImage = new byte[outH, outW];

            // 진짜 영상처리 알고리즘을 구현
            for (int i = 0; i < inH; i++)
                for (int k = 0; k < inW; k++)
                {
                    outImage[i, k] = inImage[i, inW - k - 1];
                }
            displayImage();
        }

        void updownMirror()
        {
            // 중요! 출력 이미지의 크기. --> 알고리즘에 의존.
            outH = inH;
            outW = inW;
            // 출력 이미지 메모리 할당
            outImage = new byte[outH, outW];

            // 진짜 영상처리 알고리즘을 구현
            for (int i = 0; i < inH; i++)
                for (int k = 0; k < inW; k++)
                {
                    outImage[i, k] = inImage[inH - i - 1, k];
                }
            displayImage();
        }
        void contrastImage()
        {

            // 중요! 출력 이미지의 크기. --> 알고리즘에 의존.
            outH = inH;
            outW = inW;
            // 출력 이미지 메모리 할당
            outImage = new byte[outH, outW];
            /// 진짜! 영상처리 알고리즘을 구현
            //int value = 100;
            SubForm sub = new SubForm();
            if (sub.ShowDialog() == DialogResult.Cancel)
                return;
            int value = (int)sub.numericUpDown1.Value;

            for (int i = 0; i < inH; i++)
                for (int k = 0; k < inW; k++)
                {
                    if (value > 0)
                    {
                        if ((inImage[i, k] + value) >= 255)
                            outImage[i, k] = 255;
                        else if ((inImage[i, k] - value) <= 0)
                            outImage[i, k] = 0;
                        else
                        {
                            if ((inImage[i, k]) >= 127)
                                outImage[i, k] = (byte)(inImage[i, k] + value);
                            else if ((inImage[i, k]) < 127)
                                outImage[i, k] = (byte)(inImage[i, k] - value);
                        }
                    }
                    else
                    {
                        if ((inImage[i, k] - value) >= 255)
                            outImage[i, k] = 255;
                        else if ((inImage[i, k] + value) <= 0)
                            outImage[i, k] = 0;
                        else
                        {
                            if ((inImage[i, k]) >= 127)
                                outImage[i, k] = (byte)(inImage[i, k] + value);
                            else if ((inImage[i, k]) < 127)
                                outImage[i, k] = (byte)(inImage[i, k] - value);
                        }
                    }
                    //outImage[i, k] = (byte)pixel;
                }
            displayImage();
            //MessageBox.Show(outImage[100, 100].ToString());
        }

        void posterizingImage()
        {

            // 중요! 출력 이미지의 크기. --> 알고리즘에 의존.
            outH = inH;
            outW = inW;
            // 출력 이미지 메모리 할당
            outImage = new byte[outH, outW];
            /// 진짜! 영상처리 알고리즘을 구현
            //int value = 100;


            for (int i = 0; i < inH; i++)
                for (int k = 0; k < inW; k++)
                {
                    if ((inImage[i, k]) <= 0)
                        outImage[i, k] = 0;
                    else if ((inImage[i, k] >= 0) && (inImage[i, k] < 32))
                        outImage[i, k] = 16;
                    else if ((inImage[i, k] >= 32) && (inImage[i, k] < 64))
                        outImage[i, k] = 48;
                    else if ((inImage[i, k] >= 64) && (inImage[i, k] < 96))
                        outImage[i, k] = 80;
                    else if ((inImage[i, k] >= 96) && (inImage[i, k] < 128))
                        outImage[i, k] = 112;
                    else if ((inImage[i, k] >= 128) && (inImage[i, k] < 160))
                        outImage[i, k] = 144;
                    else if ((inImage[i, k] >= 160) && (inImage[i, k] < 192))
                        outImage[i, k] = 176;
                    else if ((inImage[i, k] >= 192) && (inImage[i, k] < 224))
                        outImage[i, k] = 208;
                    else if ((inImage[i, k] >= 224) && (inImage[i, k] < 255))
                        outImage[i, k] = 240;
                    else
                        outImage[i, k] = 255;
                }
            displayImage();
            //MessageBox.Show(outImage[100, 100].ToString());
        }
        void rotateImage()
        {
            int CenterH, CenterW, newH, newW;
            double Radian, PI;


            //mReHeight = mHeight;
            //mReWidth = mWidth;
            //mReSize = mReHeight * mReWidth;

            // 중요! 출력 이미지의 크기. --> 알고리즘에 의존.
            outH = inH;
            outW = inW;
            // 출력 이미지 메모리 할당
            outImage = new byte[outH, outW];

            /// 진짜! 영상처리 알고리즘을 구현
            SubForm sub2 = new SubForm();
            if (sub2.ShowDialog() == DialogResult.Cancel)
                return;
            int degree = (int)sub2.numericUpDown1.Value;

            PI = 3.14159265358979;
            Radian = (double)degree * PI / 180.0;
            CenterH = inH / 2;
            CenterW = inW / 2;

            intempImage = new double[inH, inW];
            outtempImage = new double[outH, outW];

            for (int i = 0; i < inH; i++)
                for (int k = 0; k < inW; k++)
                {
                    intempImage[i, k] = (double)inImage[i, inW + k];
                }

            for (int i = 0; i < inH; i++)
                for (int k = 0; k < inW; k++)
                {
                    newH = (int)((i - CenterH) * Math.Cos(Radian)
                        - (k - CenterW) * Math.Sin(Radian) + CenterH);
                    newW = (int)((i - CenterH) * Math.Sin(Radian)
                        + (k - CenterW) * Math.Sin(Radian) + CenterW);

                    if (newH < 0 || newH >= inH)
                    {
                        flag = false;
                    }
                    else if (newH < 0 || newW >= inW)
                    {
                        flag = false;
                    }

                    if (flag)
                    {
                        outtempImage[i, k] = intempImage[newH, newW];
                    }

                }

            for (int i = 0; i < outH; i++)
                for (int k = 0; i < outW; k++)
                {
                    outImage[i, outW + k] = (byte)outtempImage[i, k];
                }

            //Delete[] intempImage;
            //Delete[] outtempImage;
            displayImage();
            //MessageBox.Show(outImage[100, 100].ToString());
        }


        #endregion

        #region 메뉴

        private void 열기ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                OpenFileDialog ofd = new OpenFileDialog();
                ofd.ShowDialog();

                fileName = ofd.FileName;

                // 파일의 크기를 알아내기
                long fSize = new FileInfo(fileName).Length;

                // 중요! 입력메모리의 크기를 알아냄.
                inH = inW = (int)Math.Sqrt((double)fSize);

                // 입력 메모리 확보
                inImage = new byte[inH, inW];

                // 파일 --> 입력 메모리 로딩
                BinaryReader br = new BinaryReader(File.Open(fileName, FileMode.Open));
                for (int i = 0; i < inH; i++)
                    for (int k = 0; k < inW; k++)
                        inImage[i, k] = br.ReadByte();

                br.Close();
                //MessageBox.Show(inImage[100, 100].ToString());
                //displayImage();

                equalImage();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void 저장ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            saveImage();
        }

        private void 종료ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void 동일이미지ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            equalImage();
        }

        private void 축소ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            zoomOutImage();
        }

        private void 확대ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            zoomInImage();
        }

        private void 좌우반전ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            leftrightMirror();
        }

        private void 상하반전ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            updownMirror();
        }

        private void 기본회전ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            rotateImage();
        }

        private void 반전이미지ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            reverseImage();
        }

        private void 흑백이미지ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            darkImage();
        }

        private void 밝게어둡게ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            addImage();
        }

        private void 명암대비ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            contrastImage();
        }

        private void 포스터라이징ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            posterizingImage();
        }
        #endregion
    }
}
