using System;
using wb.example;

namespace _0404_관리프로그램
{
    internal class App
    {
        private Control con;// = new Control();

        #region 싱글톤 패턴
        
		//1. 생성자를 은닉
		private App()
        {
            con = new Control();
        }

		//2. get 프로퍼티 생성(내부적으로 객체를 생성해서 저장)
		//   외부에서 객체를 획득할 수 있게 하기 위함
		public static App Singleton { get; private set; }

		//3. 정적생성자에서 객체를 생성
		static App()
        {
			Singleton = new App();
		}
	
		#endregion

		#region 실행 흐름 
		public void init()
		{
			logo();
			Console.WriteLine("\n");

			Console.Clear();
			menuprint();
		}

		public void run()
		{
			while (true)
			{
				string msg	= Wblib.getstring("");
				string[] sp1 = msg.Split(' ');

                #region 명령어 파싱
                //insert 인덱스 이름 전화번호 나이 성별
                if ( sp1[0] == "insert")
                {
					con.insert( int.Parse(sp1[1]), sp1[2], sp1[3], 
						int.Parse(sp1[4]), char.Parse(sp1[5]));
                }
				else if( sp1[0].Equals("select"))
                {
					con.select(sp1[1]);
                }
				else if(sp1[0].Equals("update"))
                {
					con.update(sp1[1], sp1[2], int.Parse(sp1[3]));
                }
				else if(sp1[0].Equals("delete"))
                {
					con.delete1(sp1[1]);
                }
				else if(sp1[0] == "printall")
                {
					con.printall();
                }
				else if(sp1[0] == "info")
                {
					menuprint();
                }
				else if(sp1[0] == "exit")
                {
					return;
                }
				else
                {
					Console.Write("잘못된 명령어입니다\n");
					menuprint();
                }
                #endregion 
            }
        }

		public void exit()
		{
			ending();
		}

		#endregion

		#region 내부 기능(실행 흐름에서 사용)
		private void logo()
		{
			Console.Clear(); 
			Console.WriteLine("***************************************************************");
			Console.WriteLine(" 비트 고급 37기\n");
			Console.WriteLine(" C#언어 과정\n");
			Console.WriteLine(" OOP 기반의 회원 관리 프로그램\n");
			Console.WriteLine(" 2023.04.04\n");
			Console.WriteLine(" 이지현\n");
			Console.WriteLine("***************************************************************");
			Wblib.Pause();
		}

		private void menuprint()
		{
			Console.WriteLine("***************************************************************");
			Console.WriteLine("[ 사용법 ]");
			Console.WriteLine("insert 인덱스 이름 전화번호 나이 성별");
			Console.WriteLine("select 이름");
			Console.WriteLine("update 이름 전화번호 나이");
			Console.WriteLine("delete 이름");
			Console.WriteLine("printall");
			Console.WriteLine("info");
			Console.WriteLine("exit");
			Console.WriteLine("***************************************************************");
		}

		private void ending()
		{
			Console.Clear();
			Console.WriteLine("***************************************************************");
			Console.WriteLine(" 프로그램을 종료합니다");
			Console.WriteLine("***************************************************************");
			Wblib.Pause();
		}

		#endregion
	}
}
