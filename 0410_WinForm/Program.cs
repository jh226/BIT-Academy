using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;

namespace _0410_WinForm
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Exam1();
        }

        
        private static void Exam1() //1. Application객체를 사용 -> MainForm을 실행
        {
            Application.Run(new Form());
        }

        private static void Exam2() //2. Form의 속성 변경
        {
            Form form = new Form();
            form.BackColor  = Color.Lime;
            //form.Opacity    = 0.7f;
            form.Text       = "타이틀바";
            form.SetBounds(10, 10, 300, 150);
            form.MinimizeBox   = false;
            form.StartPosition = FormStartPosition.CenterScreen;

            Application.Run(form);
        }

        private static void Exam3() //3.여러 개의 Form 출력
        {
            string[] strText = { "빨", "주", "노", "초", "파", "남", "보" };

            Form[] wnd = new Form[7];
            for (int i = 0; i < 7; i++)
            {
                wnd[i] = new Form();
                wnd[i].Text = strText[i];
                wnd[i].SetBounds((i + 1) * 10, (i + 1) * 10, 100, 100);
                wnd[i].MaximizeBox = false;
                wnd[i].Show();
                Console.WriteLine("{0} 번째 윈도우 출력 성공!!!", i);
            }
            Application.Run(wnd[0]);    //Application.Run에 전달되는 Form이 메인
        }

        private static void Exam4() //4. Form - 파생 클래스
        {
            Application.Run(new Form4());
        }

        private static void Exam5() //5. Form - 분할 파일구조
        {
            Application.Run(new Form5());
        }
    }
}
