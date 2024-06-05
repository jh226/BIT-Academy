using System;

namespace wb.example
{
    internal class Wblib
    {
		public static int getint(string msg)
		{			
			Console.Write(msg + " : ");
			int num = int.Parse(Console.ReadLine());
			return num;
		}

		public static char getchar(string msg)
		{
			Console.Write(msg + " : ");
			return char.Parse(Console.ReadLine());	
		}

		public static string getstring(string msg)
		{
			Console.Write(msg + " : ");
			return Console.ReadLine();
		}
	
		public static ConsoleKey getselectmenu()
        {
			ConsoleKeyInfo keyinfo = Console.ReadKey();
			ConsoleKey key = keyinfo.Key;
			return key;
        }

		public static void Pause()
        {
			Console.WriteLine("\n\n아무키나 누르세요");
			Console.ReadKey();
        }
	}
}
