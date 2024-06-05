using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using ZXing;
using OpenCvSharp;
using System.Threading;
using static System.Net.Mime.MediaTypeNames;

namespace _0411_OpenCv
{

    public partial class Form1 : Form
    {
        VideoCapture capture;
        Mat src;// = new Mat();

        public Form1()
        {
            InitializeComponent();
            capture = new VideoCapture();
            src = new Mat();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            try
            {
                capture = VideoCapture.FromCamera(VideoCaptureProperties, 0);      // 노트북일경우 0은 내장카메라 , 1은 외장카메라 // 데스크탑일 경우 처음 연결한게 0
                capture.Set(VideoCaptureProperties.FrameWidth, 480);    // 화면의 크기(pictureBoxIpl의 width보다 같거나 작으면 됨
                capture.Set(VideoCaptureProperties.FrameHeight, 300);   // 화면의 크기(pictureBoxIpl의 height보다 같거나 작으면 됨
            }
            catch
            {
                timer1.Enabled = false;
            }
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            Cv.ReleaseImage(src);
            if (src != null) src.Dispose();
        }

        public string decoded;

        private void button1_Click(object sender, EventArgs e)
        {
            pictureBox1.Image = Bitmap.FromFile("qr.png");
            pictureBox1.SizeMode = PictureBoxSizeMode.Zoom;

            BarcodeReader reader = new BarcodeReader();
            Result result = reader.Decode((Bitmap)pictureBox1.Image);
            if (result != null)
            {
                decoded = "Decode : " + result.ToString() + "\r\n Type : " + result.BarcodeFormat.ToString();
                if (decoded != "")
                {
                    textBox1.Text = decoded;
                }
            }
            else
                MessageBox.Show("바코드나 QR코드를 비추세요!");
        }

        private void timer1_Tick(object sender, EventArgs e)
        {

            src = capture.QueryFrame();
            pictureBoxIpl1.ImageIpl = src;
        }
    }
}
