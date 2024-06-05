using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Xml.Linq;
using _0503_chatingClient.ServiceReference1;
using Microsoft.Win32;
using static System.Net.WebRequestMethods;

namespace _0503_chatingClient
{
    /// <summary>
    /// MainWindow.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class MainWindow : Window, IChatCallback
    {
        string id = string.Empty;
        int idx;

        TextBox[] textbox;
        OpenFileDialog fileDlg;

        private IChat chat;

        public MainWindow()
        {
            InitializeComponent();

            textbox = new TextBox[] { textbox01, textbox02, textbox03, textbox04, textbox05, 
                textbox06, textbox07, textbox08, textbox09, textbox10, textbox11, textbox12, 
                textbox13, textbox14, textbox15, textbox16, textbox17, textbox18, textbox19, 
                textbox20, textbox21, textbox22};
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            //2 ===================================================
            InstanceContext site = new InstanceContext(this);
            chat = new _0503_chatingClient.ServiceReference1.ChatClient(site);
            btnSend.IsEnabled = false;
        }

        #region IChatCallback 인터페이스 함수 생성
        public void Receive(string id, int idx, string message)
        {
            string msgtemp = string.Format("[{0}({1})] {2}", id, idx, message);
            chatlist.Items.Add(msgtemp);
        }

        public void UserEnter(string id, int idx)
        {
            string msgtemp = string.Format("{0}({1})님이 로그인하셨습니다.", id, idx);
            chatlist.Items.Add(msgtemp);

            textbox[idx - 1].Text = id;
            textbox[idx - 1].Foreground = Brushes.White;
            textbox[idx - 1].Background = Brushes.DeepPink;
        }

        public void UserLeave(string id, int idx)
        {
            string msgtemp = string.Format("{0}({1})님이 로그아웃하셨습니다.", id, idx);
            chatlist.Items.Add(msgtemp);

            textbox[idx - 1].Text = "";
            textbox[idx - 1].Foreground = Brushes.White;
            textbox[idx - 1].Background = Brushes.White;
        }

        #endregion


        #region 로그인/로그아웃 핸들러
        private void btnJoin1_Click(object sender, RoutedEventArgs e)
        {
            if ((string)btnJoin.Content == "로그인")
            {
                this.Connect();
                btnSend.IsEnabled = true;
            }
            else this.DisConnect();
        }

        private void Connect()
        {
            try
            {
                //UI 처리
                string temp = string.Format("이름: {0}\r\n 좌석 : {1}\r\n맞습니까?", namebox.Text, seatbox.Text);
                MessageBox.Show(temp, "확인", MessageBoxButton.OKCancel);
                
                id = namebox.Text;
                idx = int.Parse(seatbox.Text);

                //서버 접속
                StudentData[] data = chat.Join1(id, idx);
                if (data == null)
                {
                    MessageBox.Show("로그인 에러");
                return;                   
                }
                //받아온 상대방들의 화면 갱신
                foreach (StudentData d in data)
                {
                    textbox[d.SeatNum - 1].Text = d.Name;
                    textbox[d.SeatNum - 1].Foreground = Brushes.White; 
                    textbox[d.SeatNum - 1].Background = Brushes.DeepPink;
                }
                btnJoin.Content = "로그아웃";
            }
            catch (Exception ex)
            {
                MessageBox.Show("접속오류" + ex.Message);
            }


            /*
            try
            {
                
                idx = int.Parse(seatbox.Text);
                id = namebox.Text;

                //서버 접속
                bool data = chat.Join(id, idx);

                btnJoin.Content = "로그아웃";
                string login = string.Format("{0}({1})님이 로그인하셨습니다.", id, seatbox.Text);
                chatlist.Items.Add(login);
            }
            catch (Exception ex)
            {
                MessageBox.Show("접속 오류 :{0}", ex.Message);
            }
            */
        }

        private void DisConnect()
        {
            try
            {
                chat.Leave1(id, idx);

                btnJoin.Content = "로그인";

                string logout = string.Format("{0}({1})님이 로그아웃하셨습니다.", id, idx);
                chatlist.Items.Add(logout);
                btnSend.IsEnabled = false;

                for(int i=0; i<19; i++)
                {
                    textbox[i].Clear();
                    textbox[i].Background = Brushes.White;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("나가기 오류 :{0}", ex.Message);
            }

        }
        #endregion

        #region 마우스 이벤트 핸들러
        // 메시지 전송
        private void btnSend_Click(object sender, RoutedEventArgs e)
        {
            string msg = msgbox.Text;

            string temp = string.Format("[{0}]", msg);
            //  chatlist.Items.Add(temp);

            chat.Say(id, idx, msg);
            msgbox.Clear();
        }
        private void Label_MouseDown(object sender, MouseButtonEventArgs e)
        {
            seatbox.Text = (string)((Label)sender).Content;
        }


        #endregion

        #region 파일 전송
        private void btnUpload_Click(object sender, RoutedEventArgs e)
        {
            //파일 전송
            //해당 이미지 파일을 스트림 형식으로 오픈한다.
            FileStream dataFileStream = new FileStream(txtUpLoad1.Text, FileMode.Open, FileAccess.Read, FileShare.Read);
            
            // 이미지 파일 스트림을 읽을 객체를 하나 만든다.
            BinaryReader dataReader = new BinaryReader(dataFileStream);
            // 파일을 바이트 배열에 넣는다.
            byte[] byteFile = { 0 };
            byteFile = dataReader.ReadBytes(Convert.ToInt32(dataFileStream.Length));
            // 파일스트림을 닫는다.
            dataFileStream.Close();

            string filename = fileDlg.SafeFileName;
            chat.UpLoadFile(id, idx, filename, byteFile);
            txtUpLoad1.Clear();

            MessageBox.Show(filename + " 파일이 업로드 되었습니다");
        }

        private void btnAllSend_Click(object sender, RoutedEventArgs e)
        {
            //파일 전송
            //해당 이미지 파일을 스트림 형식으로 오픈한다.
            FileStream dataFileStream = new FileStream(txtUpLoad1.Text, FileMode.Open, FileAccess.Read, FileShare.Read);
            
            // 이미지 파일 스트림을 읽을 객체를 하나 만든다.
            BinaryReader dataReader = new BinaryReader(dataFileStream);
            // 파일을 바이트 배열에 넣는다.
            byte[] byteFile = { 0 };
            byteFile = dataReader.ReadBytes(Convert.ToInt32(dataFileStream.Length));
            // 파일스트림을 닫는다.
            dataFileStream.Close();

            string filename = fileDlg.SafeFileName;
            chat.UpLoadFile(id, idx, filename, byteFile);
            txtUpLoad1.Clear();

            MessageBox.Show(filename + "파일이 업로드 되었습니다");
        }

        private void btnOpen_Click(object sender, RoutedEventArgs e)
        {
            fileDlg = new OpenFileDialog();

            if(fileDlg.ShowDialog() == true)
            {
                txtUpLoad1.Text = fileDlg.FileName;
            }
        }
        

        public void FileRecive(string sendername, int idx, string filename, byte[] filedata)
        {
            try
            {
                //파일10
                string path = @"C: #01_Down" + filename;
                FileStream fs = new FileStream(path, FileMode.Create, FileAccess.Write);
                fs.Write(filedata, 0, filedata.Length);
                fs.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        #endregion

        #region 박스
        private void namebox_GotFocus(object sender, RoutedEventArgs e)
        {
            namebox.Text = "";
        }
        private void seatbox_GotFocus(object sender, RoutedEventArgs e)
        {
            seatbox.Text = "";
        }
        #endregion
    }
}
