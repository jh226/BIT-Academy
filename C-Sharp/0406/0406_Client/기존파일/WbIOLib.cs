using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _0404_실습
{
    internal static class WbIOLib
    {
        public static int InputInteger(string msg)
        {
            Console.Write(msg + " : ");
            return int.Parse(Console.ReadLine());
        }

        public static int InputInteger(string msg, int start, int end)
        {
            Console.Write(msg + " : ");

            int value =  int.Parse(Console.ReadLine());
            if (value >= start && value <= end)
                return value;
            else
                throw new Exception("잘못 입력하였습니다.");
        }

        public static char InputChar(string msg)
        {
            Console.Write(msg + " : ");
            return char.Parse(Console.ReadLine());
        }

        public static double InputDouble(string msg)
        {
            Console.Write(msg + " : ");
            return double.Parse(Console.ReadLine());
        }

        public static string InputString(string msg)
        {
            Console.Write(msg + " : ");
            return Console.ReadLine();
        }

        public static ConsoleKey InputSelectMenu(string msg)
        {
            Console.Write(msg + " : ");
            return Console.ReadKey().Key;
        }
    }
}
