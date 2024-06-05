using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Diagnostics;
using System.Threading;
using System.Runtime.InteropServices;

namespace 눈코입_클라이언트
{
    public partial class Game : Form
    {

        private WbClient client;
        private Packet packet = new Packet();
        private Form1 nn = new Form1();

        Stopwatch sw;
        bool state = false;
        private Answer answer;
        int score;

        public Game(WbClient _client, string msg)
        {
            InitializeComponent();
            client = _client;
            if (client.Open(Client_RecvData) == false)
            {
                this.DialogResult = DialogResult.OK;
                this.Close();
            }

            string str =packet.Enter(Form1.name);
            client.SendData(str, str.Length);

            sw = new Stopwatch();

            score = 0;
            button3.Enabled = false;
            this.Text = msg;
            set_start_image();
        }

        private void button1_Click(object sender, EventArgs e)  //게임 시작
        {
            button1.Text = "게임시작";
            button1.Enabled = false;

            textBox2.Text = "score : " + score;

            state = true;
            string str = Packet.GAME_START;
            client.SendData(str, str.Length);
        }

        private void button2_Click(object sender, EventArgs e) // 채팅 전송
        {
            listBox1.Items.Add(Form1.name + " : " + textBox1.Text);

            string str = packet.Chat(Form1.name, textBox1.Text);
            try
            {
                client.SendData(str, str.Length);

                textBox1.Clear();
            }
            catch (Exception ee)
            {
                MessageBox.Show("실패" + ee.Message);
            }
        }
        private void button4_Click(object sender, EventArgs e) //재시작
        {
            DialogResult ret = MessageBox.Show("스코어가 초기화됩니다.\n게임을 다시 시작하겠습까?", "알림", MessageBoxButtons.YesNo);
            if (ret == DialogResult.Yes)
            {
                sw.Stop();
                sw.Reset();
                textBox3.Text = "Timer";

                listBox1.Items.Clear();
                set_start_image();
                score = 0;
                state = false;
                textBox2.Text = "score : " + score;
                button1.Enabled = true;

                string str = Packet.RESTART;
                client.SendData(str, str.Length);
            }
        }

        private void button3_Click(object sender, EventArgs e) //정답보기
        {
            answer.ShowDialog();
        }

        private void set_start_image() //시작 화면 설정
        {
            pictureBox1.Image = Image.FromFile("person1.png");
            pictureBox2.Image = Image.FromFile("person2.png");
            pictureBox3.Image = Image.FromFile("person3.png");
            pictureBox4.Image = Image.FromFile("quiz.png");
        }

        private void timer() // 타이머 설정
        {
            while(state == true)
            {
                textBox3.Text = sw.Elapsed.Hours.ToString() + "h "
                    +sw.Elapsed.Minutes +"m " + sw.Elapsed.Seconds + "s";
            }
        }
        private void textBox1_KeyDown(object sender, KeyEventArgs e)// 엔터로 전송버튼 
        {
            if (e.KeyCode == Keys.Enter) // 엔터키 처리
            {
                button2.PerformClick(); // 버튼 클릭 이벤트 실행
            }
        }

            #region 수신
            public void Client_RecvData(byte[] msg)
        {
          /*  //byte배열 실시간 수신
            ImageConverter imgcvt = new ImageConverter();
            Image img = (Image)imgcvt.ConvertFrom(msg);

            pictureBox1.Size = this.Size;
            pictureBox1.BackColor = Color.DimGray;
            pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;//PictureBoxSizeMode.AutoSize;
            pictureBox1.Image = img;
          */

            // 받은 바이트 배열을 문자열로 변환
            string receiveString = Encoding.UTF8.GetString(msg, 0, msg.Length);

            // 구분자를 이용해 헤더와 내용을 분리
            string[] dataArr = receiveString.Split(new string[] { "@" }, StringSplitOptions.None);

            // dataArr[0]에는 헤더, dataArr[1]에는 내용이 들어 있음
            string header = dataArr[0];
            string content = dataArr[1];

            if (header.Equals(Packet.IMAGE_SEND))
            {
                #region 사진 분리 후 picturbox 설정
                // 내용을 4개로 분리
                string[] image = content.Split(new string[] { "#" }, StringSplitOptions.None);

                byte[] imageBytes1 = Convert.FromBase64String(image[0]);
                byte[] imageBytes2 = Convert.FromBase64String(image[1]);
                byte[] imageBytes3 = Convert.FromBase64String(image[2]);
                byte[] imageBytes4 = Convert.FromBase64String(image[3]);

                using (MemoryStream ms = new MemoryStream(imageBytes1))
                {
                    Bitmap imageBitmap = new Bitmap(ms);
                    pictureBox1.Image = imageBitmap;
                }

                using (MemoryStream ms = new MemoryStream(imageBytes2))
                {
                    Bitmap imageBitmap = new Bitmap(ms);
                    pictureBox2.Image = imageBitmap;
                }

                using (MemoryStream ms = new MemoryStream(imageBytes3))
                {
                    Bitmap imageBitmap = new Bitmap(ms);
                    pictureBox3.Image = imageBitmap;
                }

                using (MemoryStream ms = new MemoryStream(imageBytes4))
                {
                    Bitmap imageBitmap = new Bitmap(ms);
                    pictureBox4.Image = imageBitmap;
                }

                #endregion

                sw.Start();
                Thread th = new Thread(timer);
                th.IsBackground = true;
                th.Start();
            }

            else if (header.Equals(Packet.GAME_WIN))
            {
                sw.Stop();
                listBox1.Items.Add("정답, 경과 시간 : " + sw.Elapsed);
                
                state = false;
                sw.Reset();

                MessageBox.Show("플레이어 정답");

                score += 1;
                textBox2.Text = "score : " + score;

                button1.Text = "게임 준비";
                button1.Enabled = true;
            }

           else if(header.Equals(Packet.FULL_IMAGE_SEND))
            {
                string[]data = content.Split(new string[] { "#" }, StringSplitOptions.None);

                byte[] imageData = Convert.FromBase64String(data[0]);
                Image image;
                using (MemoryStream ms = new MemoryStream(imageData))
                {
                    image = Image.FromStream(ms);
                }

                // 이미지를 바이트로 변환
                byte[] imageBytes;
                using (MemoryStream ms = new MemoryStream())
                {
                    image.Save(ms, System.Drawing.Imaging.ImageFormat.Png); // 이미지 형식에 맞게 저장
                    imageBytes = ms.ToArray();
                }
                answer = new Answer(imageBytes, data[1]);
                button3.Enabled = true;
                
            }
        }

        #endregion

    }
}
