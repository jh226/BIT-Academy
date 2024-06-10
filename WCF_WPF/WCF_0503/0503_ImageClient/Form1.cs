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
using _0503_ImageClient.ServiceReference1;

namespace _0503_ImageClient
{
    public partial class Form1 : Form
    {
        private ImageClient _imageClient = new ImageClient();

        public Form1()
        {
            InitializeComponent();
        }

        //저장
        private void button1_Click(object sender, EventArgs e)
        {
            Stream readStream;

            OpenFileDialog dlg = new OpenFileDialog();
            dlg.Filter = "그림파일 (*.bmp;*.jpg;*.gif;*.jpeg;*.png;*.tiff)|*.bmp;*.jpg;*.gif;*.jpeg;*.png;*.tiff)";
            dlg.RestoreDirectory = true;    

            if (dlg.ShowDialog() == DialogResult.OK)
            {
                if ((readStream = dlg.OpenFile()) != null)
                {
                    byte[] bytePic;
                    BinaryReader picReader = new BinaryReader(readStream);
                    bytePic = picReader.ReadBytes(Convert.ToInt32(readStream.Length));
                    FileInfo fi = new FileInfo(dlg.FileName);

                    if (_imageClient.SaveImage(fi.Name, bytePic,"111", true))
                    {
                        MessageBox.Show("업로드 성공");
                    }
                    else
                    {
                        MessageBox.Show("업로드 실패");
                    }
                    readStream.Close();
                }
            }

        }

        //데이터 리스트 가져오기
        private void button2_Click(object sender, EventArgs e)
        {
            ImageData[] imageDatas  = _imageClient.GetImageList();
            foreach(ImageData data in imageDatas)
            {
                listBox1.Items.Add(data.FileName);
            }

        }

        private void button3_Click(object sender, EventArgs e)
        {
            string filename = listBox1.SelectedItem.ToString();
            this.Text = filename;
            ImageData data = _imageClient.GetImage(filename);

            Image picImage = Image.FromStream(new MemoryStream(data.Data));
            
            pictureBox1.Image = picImage;

        }
    }
}
