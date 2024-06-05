using System;
using wb.example;

namespace _0404_관리프로그램
{
    internal class App
    {
        private Control con;// = new Control();

        public App()
        {
            con = new Control();
        }

        #region 실행 흐름 
        public void init()
		{
			logo();
		}

		public void run()
		{
			while (true)
			{
				Console.Clear();
				con.printall();
				switch (menuprint())
				{
					case ConsoleKey.D0: return;
					case ConsoleKey.D1:
					case ConsoleKey.NumPad1:	con.insert(); break;
					case ConsoleKey.D2: con.select(); break;
					case ConsoleKey.D3: con.update(); break;
					case ConsoleKey.D4: con.delete1(); break;
				}
				Wblib.Pause();
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

		private ConsoleKey menuprint()
		{
			Console.WriteLine("***************************************************************");
			Console.WriteLine("[0] 프로그램 종료");
			Console.WriteLine("[1] Insert(회원 정보 저장)");
			Console.WriteLine("[2] Select(회원 정보 검색 - 이름으로 검색, 이름은 uniq)");
			Console.WriteLine("[3] Update(회원 정보 수정 - 이름으로 검색해서 전화번호와 나이를 수정)");
			Console.WriteLine("[4] Delete(회원 정보 삭제 - 이름으로 검색해서 해당 정보를 0으로 초기화)");
			Console.WriteLine("***************************************************************");
			return Wblib.getselectmenu();  
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
