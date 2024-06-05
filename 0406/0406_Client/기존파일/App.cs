using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace _0404_실습
{
    internal class App
    {
        #region 싱글톤
        public static App Instance;
        
        static App()
        {
            Instance = new App();
        }

        private App()
        {

        }
        #endregion

        AccManager AccSel = AccManager.Instance;

        #region 실행 흐름
        public void Init()
        {
            WbPrint.Logo();
        }
        
        public void Run()
        {
            while (true)
            {
                WbPrint.Console_Cls();
                AccSel.PrintAll();
                WbPrint.MenuPrint();
                ConsoleKey key = WbIOLib.InputSelectMenu("메뉴 선택");

                switch (key)
                {
                    case ConsoleKey.F1: AccSel.MakeAccount();   break;
                    case ConsoleKey.F2: AccSel.Deposit();       break;
                    case ConsoleKey.F3: AccSel.Withdraw();      break;
                    case ConsoleKey.F4: AccSel.Inquire();       break;
                    case ConsoleKey.F5: AccSel.DeleteAccount(); break;
                    case ConsoleKey.F6: AccSel.Sort_AccID();    break;
                    case ConsoleKey.F7: AccSel.Sort_Balacne();  break;
                    case ConsoleKey.F8: return;
                    default: Console.WriteLine("선택을 잘못하셨습니다.");  break;
                }
                Thread.Sleep(1000);

                WbPrint.Console_Pause();
            }
        }

        public void Exit()
        {
            WbPrint.Ending();
        }
        #endregion 
    }
}
