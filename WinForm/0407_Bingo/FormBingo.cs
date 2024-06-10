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
    public partial class FormBingo : Form
    {
        //번호 관리(초기화:LButtonDown,버튼핸들러, 사용(set), 사용(get):OnPaint)
        private int[,] iGame = new int[5, 5];
        //선택 관리(초기화, 사용(set), 사용(get))
        private bool[,] bGame = new bool[5,5];

        public bool IsActive { get; set; } = false;   //자신의 차례
        public bool IsUser { get; set; }

        private Form1 form_parent = null;
                
        public int NumberCount { get; private set; }

        public int State { get; private set; } = 0;  //0:숫자초기화전 1:숫자초기화 2:게임중
                

        #region 부모로 부터 메시지 수신
        public void SetActive()
        {
            IsActive = true;

            if (IsUser == false) //PC라면
            {
                //Thread.Sleep(2000);

                Random r = new Random();
                int number = -1;
                int row = -1, col = -1;
                while (true)
                {
                    number = r.Next(1, 26); //1~25
                    if (IsNumberCheck(number, ref row, ref col) == true)
                        break;
                }

                //내 정보를 갱신
                DrawCheck(row, col);

                //.....
                //결과체크
                if (IsGameEnd() == true)
                {
                    form_parent.Winner(this);
                }

                //상대방 정보를 갱신
                form_parent.SelectNumber(this, iGame[row, col]);
                               

                IsActive = false;
            }
        }
        
        private bool IsNumberCheck(int number, ref int row, ref int col)
        {
            for (int i = 0; i < 5; i++)
            {
                for (int j = 0; j < 5; j++)
                {
                    if (number == iGame[i, j])
                    {
                        if (bGame[i, j] == true)
                            return false;
                        else
                        {
                            row = i;
                            col = j;
                            return true;
                        }
                    }
                }
            }
            return false;
        }
        
        #endregion

        #region 객체 초기화
        public FormBingo(Form1 p, Point pt)
        {
            InitializeComponent();
            NumberCount = 1;

            this.FormBorderStyle = FormBorderStyle.None;
            //this.Size = new Size(270, 270);  
            for (int i = 0; i < 5; i++)
            {
                for (int j = 0; j < 5; j++)
                    bGame[i, j] = false;
            }

            InitForm(p, pt);
        }
        private void InitForm(Form1 parent, Point pt)
        {
            TopLevel = false;
            parent.Controls.Add(this);
            Parent = parent;
            form_parent = parent;
            Location = pt;
            Show();
        }
        #endregion 

        #region 화면구성(5*5 사각형 그리기)

        // 사각형을 그려준다.(250 * 250  시작은 (10, 10))
        private void DrawRec(Graphics gh)
        {
            gh.FillRectangle(new SolidBrush(Color.FromArgb(62, 62, 124)), 10, 10, 250, 250);
        }

        // 구분선을 그려준다.(5 * 5, 한칸은 50 픽셀 크기)
        private void DrawLine(Graphics gh)
        {
            //가로선 그리기
            for (int i = 0; i < 6; i++) 
            {
                gh.DrawLine(new Pen(Color.White), 10, 10 + i * 50, 50 * 5 + 10, 10 + i * 50);
            }

            //세로선 그리기
            for (int i = 0; i < 6; i++) 
            {
                gh.DrawLine(new Pen(Color.White), 10 + i * 50, 10, 10 + i * 50, 50 * 5 + 10);
            }
        }

        // 숫자를 그려준다.
        private void DrawNum(Graphics gh, int iRow, int iCol, int iNum)
        {
            string str = string.Format("{0}", iNum);

            //선택여부에 따른 색상 출력
            if( bGame[iRow, iCol] == true)
                gh.FillRectangle(new SolidBrush(Color.FromArgb(124, 0, 0)),
                    10 + iCol * 50, 10 + iRow * 50, 50, 50);

            //숫자 출력
            if (IsUser == false)
                return;

            if (str.Length > 1)
                gh.DrawString(str, Font, new SolidBrush(Color.FromArgb(255, 255, 255)),
                    27 + iCol * 50, 30 + iRow * 50);
            else
                gh.DrawString(str, Font, new SolidBrush(Color.FromArgb(255, 255, 255)),
                   30 + iCol * 50, 30 + iRow * 50);
        }

        private void DrawNum(int iRow, int iCol, int iNum)
        {
            iGame[iRow, iCol] = iNum;

            this.Invalidate(new Rectangle(10 +iCol*50, 10+iRow*50, 50, 50));            
        }
        #endregion

        #region 게임초기화

        //포지션을 이용해 인덱스를 구한다.
        void PosToIndex(Point pnt, out int row, out int col)
        {
            row = -1;
            col = -1;
                
            for (int i = 0; i < 5; i++) //행 결정(Row)
            {
                if ((pnt.Y > 10 + i * 50) && pnt.Y <= (10 + (i + 1) * 50))
                {
                    row = i;
                    break;
                }
            }

            for (int j = 0; j < 5; j++) //열 결정(Col)
            {
                if ((pnt.X > 10 + j * 50) && pnt.X <= (10 + (j + 1) * 50))
                {
                    col = j;
                    break;
                }
            }
        }

        //번호 설정(사용자 입력):마우스를 클릭하는 순서대로 번호를 매긴다.
        private void OrderNum(int row, int col)
        {
            if (iGame[row, col] != 0)
                return;

            iGame[row,col] = NumberCount++;
            DrawNum(row, col, iGame[row,col]);
        }
        
        //번호 설정(자동) : 버튼 클릭시 
        private void RandomNum()
        {
            int number = 1;
            for (int i = 0; i < 5; i++)
            {
                for (int j = 0; j < 5; j++)
                    iGame[i, j] = number++;
            }

            Random r = new Random();
            for (int i = 0; i < 500; i++)
            {
                int row1 = r.Next(5);
                int col1 = r.Next(5);
                int row2 = r.Next(5);
                int col2 = r.Next(5);

                int temp = iGame[row1, col1];
                iGame[row1, col1] = iGame[row2, col2];
                iGame[row2, col2] = temp;
            }
            this.Invalidate();
            label1.Text = "숫자초기화 완료";
            State = 1;
        }

        #endregion

        #region 게임진행
        // 전달된 인덱스에 표시를 한다
        private void DrawCheck(int row, int col)
        {
            //1. 데이터 처리
            bGame[row, col] = true;
            Invalidate(new Rectangle(10 + col * 50, 10 + row * 50, 50, 50));
        }        
        public void SelectNumber(int number)
        {
            for(int i=0; i< 5; i++)
            {
                for (int j = 0; j < 5; j++)
                {
                    if (iGame[i, j] == number)
                    {
                        DrawCheck(i, j);
                        break;
                    }
                }
            }
            //결과체크
            if (IsGameEnd() == true)
            {
                form_parent.Winner(this);
            }
        }
        private bool IsGameEnd()
        {
            int iLine = 0; //개수
            int tempcount = 0;

            // 가로 검사
            for (int i = 0; i < 5; i++) 
            {
                tempcount = 0;   
                for (int j = 0; j < 5; j++)
                {
                    if (bGame[i,j] == false)
                        break;
                    else
                        tempcount++;
                }
                if (tempcount == 5)
                    iLine++;
            }

            // 세로 검사
            for (int i = 0; i < 5; i++) 
            {
                tempcount = 0;
                for (int j = 0; j < 5; j++)
                {
                    if (bGame[j, i] == false)
                        break;
                    else
                        tempcount++;
                }
                if (tempcount == 5)
                    iLine++;
            }

            // 대각선 검사(＼ 방향)
            tempcount = 0;
            for (int i = 0; i < 5; i++) 
            {
                if (bGame[i, i] == false)
                    break;
                else
                    tempcount++;
            }
            if (tempcount == 5)
                iLine++;

            // 대각선 검사(／ 방향) 0,4  1,3  2,2  3,1  4,0
            tempcount = 0;
            for (int i = 0, j = 4; i < 5; i++, j--) 
            {
                if (bGame[i, j] == false)
                    break;
                else
                    tempcount++;
            }
            if (tempcount == 5)
                iLine++;

            // 5줄 이상이면 빙고
            if (iLine >= 2)
                return true;
            else
                return false;
        }
        #endregion

        private void FormBingo_Paint(object sender, PaintEventArgs e)
        {
            DrawRec(e.Graphics);            

            for (int i = 0; i < 5; i++)
            {
                for (int j = 0; j < 5; j++)
                {
                     DrawNum(e.Graphics, i, j, iGame[i,j]);
                }
            }

            DrawLine(e.Graphics);
        }

        private void FormBingo_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.X > 260 || e.Y > 260) // 게임과 관련 없는곳 클릭시
                return;
            if (e.X < 10 || e.Y < 10)
                return;

            //마우스 좌표 --> 배열 인덱스
            int row, col;
            PosToIndex(e.Location, out row, out col);

            // 게임 시작전
            if (State == 0) //숫자초기화 전
            {
                OrderNum(row, col);
                if (NumberCount == 26)
                {
                    label1.Text = "숫자초기화 완료";
                    NumberCount = 0;
                    State = 1;
                }
            }
            else if (State == 1)  //숫자초기화 완료
            {

            }
            else if (State == 2) //게임중
            {
                if (IsActive == false)
                    return;

                if (bGame[row, col] == false)
                {
                    IsActive = false;

                    //내 정보를 갱신
                    DrawCheck(row, col);

                    //결과체크
                    if (IsGameEnd() == true)
                    {
                        form_parent.Winner(this);
                    }

                    //상대방 정보를 갱신
                    form_parent.SelectNumber(this, iGame[row, col]);
                }
            }
        }

        //번호 자동 초기화 
        private void button1_Click(object sender, EventArgs e)
        {
            RandomNum();
        }

        //게임시작
        private void button2_Click(object sender, EventArgs e)
        {
            if(State == 0)
            {
                MessageBox.Show("숫자 초기화가 필요합니다");
                return;
            }

            State = 2;
            label1.Text = "게임 준비 완료";
            form_parent.Send_UserCount(this);
        }
    }
}
