using System;
using System.Collections.Generic;
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
using _0503_ChatClient.ServiceReference1;

namespace _0503_ChatClient
{
    /// <summary>
    /// MainWindow.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class MainWindow : Window, IChatCallback
    {
        public string NickName 
        { 
            get { return seatbox.Text;  } 
            set { seatbox.Text = value; }
        }
        public string Message
        {
            get { return msgbox.Text; }
            set { msgbox.Text = value; }
        }

        private ChatClient chat;

        public MainWindow()
        {
            InitializeComponent();

            InstanceContext site = new InstanceContext(this);
            chat = new ChatClient(site);
        }


        #region (서비스 -> 클라이언트)
        public void Receive(string nickname, string message)
        {
            string msgtemp = string.Format("[{0}] {1}", nickname, message);
            chatlist.Items.Add(msgtemp);
        }

        public void UserEnter(string nickname)
        {
            string msgtemp = string.Format("{0}님이 로그인하셨습니다.", nickname);
            chatlist.Items.Add(msgtemp);
        }
        #endregion

        #region 버튼 핸들러
        //접속
        private void btnJoin_Click(object sender, RoutedEventArgs e)
        {
            if ((string)btnJoin.Content == "로그인") 
                this.Connect();
            else 
                this.DisConnect();
        }

        //전송
        private void btnSend_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                chat.Say(NickName, Message);
                Message = "";
                msgbox.Focus();
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message ,"로그인을 먼저");
            }
        }

        #endregion 

        private void Connect()
        {
            try
            {
                //서버 접속
                if (chat.Join(NickName) == false)
                    throw new Exception("접속 실패");

                btnJoin.Content = "로그아웃";
            }
            catch (Exception ex)
            {
                MessageBox.Show("{0}", ex.Message);
            }
        }

        private void DisConnect()
        {
            try
            {
                chat.Leave(NickName);

                btnJoin.Content = "로그인";

                string logout = string.Format("{0}님이 로그아웃하셨습니다.", seatbox.Text);
                chatlist.Items.Add(logout);
            }
            catch (Exception ex)
            {
                MessageBox.Show("나가기 오류 :{0}", ex.Message);
            }
        }

        //컨트롤 처리
        private void seatbox_GotFocus(object sender, RoutedEventArgs e)
        {
            NickName = "";
        }
    }
}
