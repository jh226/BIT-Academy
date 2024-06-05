using OpenCvSharp;
using OpenCvSharp.Dnn;
using OpenCvSharp.Extensions;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Markup;

namespace 눈코입_서버
{
    public partial class Form1 : Form
    {
        private List<string> player_name = new List<string>();
        private WbServer server = new WbServer(7000);
        private Packet packet = new Packet();

        int player;
        int player_count;
        int game_ready;
        bool state = false;
        string answer;

        #region 생성자, 초기화 
        public Form1()
        {
            InitializeComponent();
            set_start_image();
            player = 2;
            player_count = 0;
            game_ready = 0;
        }

        private void set_start_image()
        {
            pictureBox1.Image = null;
            pictureBox2.Image = Image.FromFile("person1.png");
            pictureBox3.Image = Image.FromFile("person2.png");
            pictureBox4.Image = Image.FromFile("person3.png");
            pictureBox5.Image = Image.FromFile("quiz.png");
        }
        #endregion

        #region 문제 출제, 얼굴인식
        private void button2_Click(object sender, EventArgs e) // 파일 열기 + 얼굴 인식
        {
            // 파일 열기
            if(state == true)
            {
                try
                {
                    string filePath = string.Empty;
                    var fileName = string.Empty;

                    using (OpenFileDialog fd = new OpenFileDialog())
                    {
                        //바탕화면으로 기본폴더 설정
                        fd.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);

                        if (fd.ShowDialog() == DialogResult.OK)
                        {
                            filePath = fd.FileName; //전체 경로와 파일명 //선택한 파일명은 fd.SafeFileName
                            fileName = fd.SafeFileName; // 파일명은 fd.SafeFileName
                        }
                        Image I = Image.FromFile(filePath);
                        pictureBox1.Image = Bitmap.FromFile(filePath);

                        //얼굴 인식 
                        face_detect();
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
           else
            {
                listBox1.Items.Add("플레이어가 준비되지 않았습니다");
            }
        }

        private void face_detect() //얼굴 인식
        {
            try
            {
                int i = 0;  //눈 왼,오 구분

                String filenameFaceCascade = "haarcascade_frontalface_alt.xml";
                String filenameEyeCascade = "haarcascade_eye.xml";
                String filenameNoseCascade = "haarcascade_mcs_nose.xml";
                String filenameMouthCascade = "harcascademouth.xml";

                CascadeClassifier faceCascade = new CascadeClassifier(filenameFaceCascade);
                CascadeClassifier eyesCascade = new CascadeClassifier(filenameEyeCascade);
                CascadeClassifier nosesCascade = new CascadeClassifier(filenameNoseCascade);
                CascadeClassifier mouthCascade = new CascadeClassifier(filenameMouthCascade);


                Mat imgMat = OpenCvSharp.Extensions.BitmapConverter.ToMat((Bitmap)pictureBox1.Image);

                if (!faceCascade.Load(filenameFaceCascade) || !eyesCascade.Load(filenameEyeCascade)
                    || !nosesCascade.Load(filenameNoseCascade) || !mouthCascade.Load(filenameMouthCascade))
                {
                    Console.WriteLine("Load Error");
                    return;
                }

                Rect[] faces = faceCascade.DetectMultiScale(imgMat, 1.3, 5);

                Rect[] eyes = eyesCascade.DetectMultiScale(imgMat, 1.3, 20);

                Rect[] noses = nosesCascade.DetectMultiScale(imgMat, 1.3, 30);

                Rect[] mouth = mouthCascade.DetectMultiScale(imgMat, 1.3, 40);


                foreach (var item in faces)
                {
                    Cv2.Rectangle(imgMat, item, Scalar.Red); // add rectangle to the image
                    Console.WriteLine("faces : " + faces.Length + item);
                    Cv2.PutText(imgMat, "face", new OpenCvSharp.Point(item.X, item.Y),
                        HersheyFonts.HersheyPlain, 0.9/*font size*/, Scalar.Red, 2);

                }

                foreach (var item in eyes)
                {
                    Cv2.Rectangle(imgMat, item, Scalar.Green); // add rectangle to the image
                    Console.WriteLine("eyes : " + eyes.Length + item);
                    Cv2.PutText(imgMat, "eyes", new OpenCvSharp.Point(item.X, item.Y),
                        HersheyFonts.HersheyPlain, 0.9/*font size*/, Scalar.Green, 2);

                    Rect eye = new Rect(item.X, item.Y, item.Width, item.Height);
                    Mat faceMat = imgMat.SubMat(eye);

                    if (i == 0)
                    {
                        pictureBox2.Image = faceMat.ToBitmap();
                        i++;
                        continue;
                    }
                    else if (i == 1)
                    {
                        pictureBox4.Image = faceMat.ToBitmap();
                    }

                }

                foreach (var item in noses)
                {
                    Cv2.Rectangle(imgMat, item, Scalar.Blue); // add rectangle to the image
                    Console.WriteLine("noses : " + noses.Length + item);
                    Cv2.PutText(imgMat, "nose", new OpenCvSharp.Point(item.X, item.Y),
                        HersheyFonts.HersheyPlain, 0.3/*font size*/, Scalar.Blue, 1);

                    Rect nose = new Rect(item.X, item.Y, item.Width, item.Height);
                    Mat faceMat = imgMat.SubMat(nose);

                    pictureBox3.Image = faceMat.ToBitmap();
                }

                foreach (var item in mouth)
                {
                    Cv2.Rectangle(imgMat, item, Scalar.Yellow); // add rectangle to the image
                    Console.WriteLine("mouth : " + mouth.Length + item);
                    Cv2.PutText(imgMat, "mouth", new OpenCvSharp.Point(item.X, item.Y),
                        HersheyFonts.HersheyPlain, 0.3/*font size*/, Scalar.Yellow, 2);

                    Rect mouth1 = new Rect(item.X, item.Y, item.Width, item.Height);
                    Mat faceMat = imgMat.SubMat(mouth1);

                    pictureBox5.Image = faceMat.ToBitmap();
                }
                // display
                pictureBox1.Image = BitmapConverter.ToBitmap(imgMat);
            }

            catch (Exception ex)
            {
                MessageBox.Show("이미지를 먼저 불러오세요.\n" + ex.Message);
            }
        }

        private void button1_Click(object sender, EventArgs e) // 문제출제(정답 설정) + 눈코입, 정답 전송
        {
            #region 정답 설정
            answer = string.Empty;
            try
            {
                answer = textBox1.Text;
            }
            catch(Exception ee)
            {
                MessageBox.Show("정답을 입력해주세요\n" + ee.Message);
            }
            #endregion

            #region 눈코입 이미지 전송
            Bitmap LeftEye_Image = (Bitmap)pictureBox2.Image;
            Bitmap RightEye_Image = (Bitmap)pictureBox3.Image;
            Bitmap Nose_Image = (Bitmap)pictureBox4.Image;
            Bitmap Mouth_Image = (Bitmap)pictureBox5.Image;

            using (MemoryStream ms1 = new MemoryStream())
            using (MemoryStream ms2 = new MemoryStream())
            using (MemoryStream ms3 = new MemoryStream())
            using (MemoryStream ms4 = new MemoryStream())
            {
                LeftEye_Image.Save(ms1, ImageFormat.Jpeg);
                RightEye_Image.Save(ms2, ImageFormat.Jpeg);
                Nose_Image.Save(ms3, ImageFormat.Jpeg);
                Mouth_Image.Save(ms4, ImageFormat.Jpeg);

                byte[] imageData1 = ms1.ToArray();
                byte[] imageData2 = ms2.ToArray();
                byte[] imageData3 = ms3.ToArray();
                byte[] imageData4 = ms4.ToArray();

                string msg = packet.Image_Send(imageData1, imageData2, imageData3, imageData4);
                
                server.SendAllData(Encoding.UTF8.GetBytes(msg), msg.Length); 
            }
            #endregion

            #region 정답 사진 전송
            Image image = pictureBox1.Image;

            // 이미지를 바이트 배열로 변환
            byte[] byteArray;
            using (MemoryStream stream = new MemoryStream())
            {
                image.Save(stream, System.Drawing.Imaging.ImageFormat.Png);
                byteArray = stream.ToArray();
            }

            string msg1 = packet.Full_Image_Send(byteArray, answer);
            byte[] fullimage = Encoding.ASCII.GetBytes(msg1);
            server.SendAllData(fullimage, fullimage.Length);

            #endregion
        }
        #endregion

        

        #region 네트워크 데이터 수신 
        public void Server_RecvData(Socket sock, string msg)
        {
            string[] str = msg.Split(new string[] { "@" }, StringSplitOptions.None);

            if (str[0].Equals(Packet.ENTER))
            {
                //플레이어 수가 다 차지 않은경우
                if( player_count < player)
                {
                    player_count++;
                    listBox1.Items.Add(str[1] + "님, 입장");
                    player_name.Add(str[1]);    //이름 저장

                    string str1 = packet.Enter(true, str[1], player_count);

                    byte[] msg1 = Encoding.UTF8.GetBytes(str1);
                    server.SendAllData(msg1, msg1.Length);
                }

                //플레이어 수가 꽉찬 경우
                else
                {
                    string str1 = packet.Enter(false, str[1], player_count);

                    byte[] msg1 = Encoding.UTF8.GetBytes(str1);
                    server.SendAllData(msg1, msg1.Length);
                }
            }
            else if (str[0].Equals(Packet.GAME_START))
            {
                game_ready++;
                listBox1.Items.Add(str[1] + ", 준비 완료");

                string chat_msg = packet.Chat(str[1], " 준비 완료");
                byte[] chat_data = Encoding.UTF8.GetBytes(chat_msg);
                server.SendAllData(chat_data, chat_data.Length);

                if (player == game_ready) 
                {
                    state = true;
                    string start_msg = packet.Game_Start_Ack();
                    byte[] start_data = Encoding.UTF8.GetBytes(start_msg);
                    server.SendAllData(start_data, start_data.Length);
                }
            }
            else if(str[0].Equals(Packet.CHAT))
            {
                string[] content = str[1].Split(new string[] { "#" }, StringSplitOptions.None);

                string chat_msg = packet.Chat(content[0], content[1]);
                byte[] chat_data = Encoding.UTF8.GetBytes(chat_msg);
                server.SendAllData(chat_data, chat_data.Length);

                listBox1.Items.Add(content[0] + " : " + content[1]);

                if (content[1].Equals(answer))
                {
                    byte[] msg1 = Encoding.UTF8.GetBytes(packet.Game_Win(content[0]));
                    server.SendAllData(msg1, msg1.Length);

                    listBox1.Items.Add(content[0] + "님, 정답");
                    MessageBox.Show(content[0] + "님이 정답을 맞췄습니다.");
                    state = false;
                }
            }
            else if (str[0].Equals(Packet.RESTART))
            {
                listBox1.Items.Add("플레이어 재시작");
                listBox1.Items.Clear();
                state = false;
                set_start_image();
            }
        }
        #endregion

        #region 폼 Load & Closed 시 서버 소켓 실행 및 종료
        private void Form1_Load(object sender, EventArgs e)
        {
            server.Start(Server_RecvData);
            this.Text = "서버 실행중( 127.0.0.1 : 7000 )....";
        }

        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {
            server.Stop();
            this.Text = "서버 종료";
        }

        #endregion

    }
}
