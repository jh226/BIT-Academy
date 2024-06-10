﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
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

        private MemberManager mm = MemberManager.Instance;
        private MemberLog log = new MemberLog();

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
                mm.PrintAll();
                WbPrint.MenuPrint();
                ConsoleKey key = WbIOLib.InputSelectMenu("메뉴 선택");

                switch (key)
                {
                    case ConsoleKey.F1: mm.AddMember();          break;
                    case ConsoleKey.F2: mm.SelectMember();       break;
                    case ConsoleKey.F3: mm.Update();             break;
                    case ConsoleKey.F4: mm.Delete();       break;
                    case ConsoleKey.F8: return;
                    default: Console.WriteLine("선택을 잘못하셨습니다.");  break;
                }
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
