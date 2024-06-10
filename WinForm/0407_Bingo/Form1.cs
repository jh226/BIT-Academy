using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace _1018_Bingo
{
    public partial class Form1 : Form
    {
        private FormBingo[] users = new FormBingo[5];

        public int UserCount { get; private set; } = 0;

        public int ActiveUser { get; private set; }   //0, 1

        public bool IsResult { get; private set; } = false;

        public Form1()
        {
            InitializeComponent();
            users[0] = new FormBingo(this, new Point(10, 30));
            users[0].IsUser = true;

            users[1] = new FormBingo(this, new Point(500, 30));
            users[1].IsUser = false;

            users[2] = new FormBingo(this, new Point(1000, 30));
            users[2].IsUser = false;

            users[3] = new FormBingo(this, new Point(10, 330));
            users[3].IsUser = false;

            users[4] = new FormBingo(this, new Point(500, 330));
            users[4].IsUser = false;
        }

        //준비되었다는 신호를 받는다.
        public void Send_UserCount(FormBingo user)
        {
            textBox1.Text = "user 준비완료.................";
            UserCount++;

            if(UserCount == 5)
            {
                textBox1.Text = "게임 시작..................";
                //Thread.Sleep(2000);

                //게임시작 명령을 전달
                Random random = new Random();
                ActiveUser = random.Next(0, 5);
                if (ActiveUser == 0)
                {
                    textBox1.Text = "user차례입니다..................";
                    users[0].SetActive();                    
                }
                else if(ActiveUser == 1)
                {
                    textBox1.Text = "PC1차례입니다...................";
                    //users[1].SetActive();                    
                }
                else if (ActiveUser == 2)
                {
                    textBox1.Text = "PC2차례입니다...................";
                    //users[2].SetActive();
                }
                else if (ActiveUser == 3)
                {
                    textBox1.Text = "PC3차례입니다...................";
                    //users[3].SetActive();
                }
                else if (ActiveUser == 4)
                {
                    textBox1.Text = "PC4차례입니다...................";
                    //users[4].SetActive();
                }
            }
        }


        //클릭된 좌표 정보를 획득
        public void SelectNumber(FormBingo user, int number)
        {
            if(users[0] == user)
            {
                textBox1.Text = "User가 번호를 입력하였습니다";
                textBox1.Text = "PC1차례입니다..................";

                for (int i = 0; i<5; i++)
                {
                    if (i == 0)
                        continue;
                    users[i].SelectNumber(number);
                }
                
                //if(IsResult == false)
                    //users[1].SetActive();                 
            }
            else if (users[1] == user)
            {
                textBox1.Text = "PC1이 번호를 입력하였습니다";
                textBox1.Text = "PC2차례입니다..................";

                for (int i = 0; i < 5; i++)
                {
                    if (i == 1)
                        continue;
                    users[i].SelectNumber(number);
                }

                //if (IsResult == false)
                    //users[2].SetActive();
            }
            else if (users[2] == user)
            {
                textBox1.Text = "PC2이 번호를 입력하였습니다";
                textBox1.Text = "PC3차례입니다..................";

                for (int i = 0; i < 5; i++)
                {
                    if (i == 2)
                        continue;
                    users[i].SelectNumber(number);
                }

                //if (IsResult == false)
                //    users[3].SetActive();
            }
            else if (users[3] == user)
            {
                textBox1.Text = "PC3이 번호를 입력하였습니다";
                textBox1.Text = "PC4차례입니다..................";

                for (int i = 0; i < 5; i++)
                {
                    if (i == 3)
                        continue;
                    users[i].SelectNumber(number);
                }

                //if (IsResult == false)
                //    users[4].SetActive();
            }
            else if (users[4] == user)
            {
                textBox1.Text = "PC4이 번호를 입력하였습니다";
                textBox1.Text = "User차례입니다..................";

                for (int i = 0; i < 5; i++)
                {
                    if (i == 4)
                        continue;
                    users[i].SelectNumber(number);
                }

                if (IsResult == false)
                    users[0].SetActive();
            }
        }

        public void Winner(FormBingo user)
        {
            if (users[0] == user)
            {
                MessageBox.Show("내가 승리");
            }
            else
            {
                MessageBox.Show("컴퓨터 승리");
            }
            IsResult = true;
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            textBox1.AppendText(Environment.NewLine + textBox2.Text + Environment.NewLine);
            textBox2.Clear();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            users[1].SetActive();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            users[2].SetActive();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            users[3].SetActive();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            users[4].SetActive();
        }
    }
}
