using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _0404_실습
{
    internal static class WbPrint
    {
        public static void Logo()
        {
            Console_Cls();
            Console.WriteLine("********************************************");
            Console.WriteLine("우송비트 고급 37기");
            Console.WriteLine("C#언어 기반 프로그래밍");
            Console.WriteLine("2023-04-05");
            Console.WriteLine("********************************************");
            Console_Pause();
        }

        public static void Ending()
        {
            Console_Cls();
            Console.WriteLine("********************************************");
            Console.WriteLine("프로그램을 종료합니다.");
            Console.WriteLine("********************************************");
            Console_Pause();
        }

        public static void Console_Pause()
        {
            Console.WriteLine("\n\n아무키나 누르세요.......");
            Console.ReadKey();
        }

        public static void Console_Cls()
        {
            Console.Clear();
        }

        public static void MenuPrint()
        {
            Console.WriteLine("***** 메뉴 목록 *****");
            Console.WriteLine("[F1] 회원 등록");
            Console.WriteLine("[F2] 회원 검색");
            Console.WriteLine("[F3] 회원 정보 수정");
            Console.WriteLine("[F4] 회원 삭제");
            Console.WriteLine("[F8] 종     료");
            Console.WriteLine("********************");
        }
    }
}
