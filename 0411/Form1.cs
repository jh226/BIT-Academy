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
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        // 전역 변수
        byte[,] inImage, outImage;
        int inH = 0, inW = 0, outH = 0, outW = 0;
        String fileName;
        Bitmap bitmap;

        #region 기능
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
        }

        void equalImage() //원래대로
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
        void reverseImage() //반전
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

        void lightVolumUp() //밝게
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
                    int pixel = inImage[i, k] + 80;
                    if (pixel > 255)
                        pixel = 255;
                    else if (pixel < 0)
                        pixel = 0;

                    outImage[i, k] = (byte)pixel;
                }
            displayImage();
        }

        void lightVolumDown() //어둡게
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
                    int pixel = inImage[i, k] - 80;
                    if (pixel > 255)
                        pixel = 255;
                    else if (pixel < 0)
                        pixel = 0;

                    outImage[i, k] = (byte)pixel;
                }
            displayImage();
        }

        void leftrightMirror() //좌우반전
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

        void updownMirror()// 상하반전
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

        void enlargeImage() //확대
        {
            // 중요! 출력 이미지의 크기. --> 알고리즘에 의존.
            outH = 2 * inH;
            outW = 2 * inW;
            // 출력 이미지 메모리 할당
            outImage = new byte[outH, outW];
            int ary_index, ary_outdex;

            for (int i = 0; i < inH; i++)
            {
                ary_outdex = 0;
                ary_index = i * 2;
                for (int k = 0; k < inW; k++)
                {
                    outImage[ary_index, ary_outdex] = inImage[i, k];
                    ary_outdex++;
                    outImage[ary_index, ary_outdex] = inImage[i, k];
                    ary_index++;
                    outImage[ary_index, ary_outdex] = inImage[i, k];
                    ary_outdex--;
                    outImage[ary_index, ary_outdex] = inImage[i, k];
                    ary_index--; ary_outdex++; ary_outdex++;
                }

            }
            displayImage();
        }

        void reduceImage() //축소
        {
            // 중요! 출력 이미지의 크기. --> 알고리즘에 의존.
            outH = inH / 2;
            outW = inW / 2;
            // 출력 이미지 메모리 할당

            outImage = new byte[outH, outW];
            for (int i = 0; i < inH; i++)
            {
                if (i % 2 == 1) continue;

                for (int k = 0; k < inW; k++)
                {
                    if (k % 2 == 1) continue;
                    outImage[i / 2, k / 2] = inImage[i, k];
                }
            }
            displayImage();
        }

        void WtoBImage() //흑백
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
                    int pixel = inImage[i, k];
                    if (pixel < 127)
                        pixel = 0;
                    else if (pixel > 127)
                        pixel = 255;

                    outImage[i, k] = (byte)pixel;
                }

            displayImage();
            //MessageBox.Show(outImage[100, 100].ToString());
        }
        #endregion


        #region 메뉴

        private void 열기ToolStripMenuItem_Click_1(object sender, EventArgs e)
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

        private void 원래대로ToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            equalImage();
        }

        private void 흑백처리ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            WtoBImage();
        }

        private void 미러링ToolStripMenuItem_Click_1(object sender, EventArgs e)
        {

        }

        private void 상하반전ToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            updownMirror();
        }

        private void 좌우반전ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            leftrightMirror();
        }

        private void 밝게ToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            lightVolumUp();
        }

        private void 반전ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            reverseImage();
        }

        private void 어둡게ToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            lightVolumDown();
        }

        private void 확대ToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            enlargeImage();
        }

        private void 축소ToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            reduceImage();
        }

        private void 종료ToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            this.Close();
        }
        #endregion
    }
}
