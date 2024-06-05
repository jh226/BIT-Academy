using _0404_실습.Event;
using _0404_실습.input;
using _0404_실습.Lib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _0404_실습.manger
{
    internal class AccountLog
    {
        public AccountLog()
        {
            AccManager mm = AccManager.Instance;
            mm.MakeAccountEventHandler += new MakeAccountEvent(OnMakeAccountEventHandler);
            mm.DepositEventHandler += new DepositEvent(OnDepositEventHandler);
            mm.WithdrawEventHandler += new WithdrawEvent(OnWithdrawEventHandler);
            mm.SelectEventHandler += new SelectEvent(OnSelectEventHandler);
            mm.DeleteEventHandler += new DeleteEvent(OnDeleteEventHandler);
           
        }

        public void OnMakeAccountEventHandler(object obj,MakeAccountEventArgs e)
        {
            Console.WriteLine("[회원가입로그]");
            Console.WriteLine(e.Id + ", " + e.Name + ", "+ e.Balance);
            Console.WriteLine(e.Date.ToString());
        }

        public void OnDepositEventHandler(object obj, DepositEventArgs e)
        {
            Console.WriteLine("[입금 로그]");
            Console.WriteLine(e.Id + ", " + e.Input);
            Console.WriteLine(e.Date.ToString());
        }
        public void OnWithdrawEventHandler(object obj, WithdrawEventArgs e)
        {
            Console.WriteLine("[출금 로그]");
            Console.WriteLine(e.Id + ", " + e.Output);
            Console.WriteLine(e.Date.ToString());
        }
        public void OnSelectEventHandler(object obj, SelectEventArgs e)
        {
            Console.WriteLine("[회원검색로그]");
            Console.WriteLine(e.Id);
            Console.WriteLine(e.Date.ToString());
        }
        public void OnDeleteEventHandler(object obj, DeleteEventArgs e)
        {
            Console.WriteLine("[회원삭제로그]");
            Console.WriteLine(e.Id);
            Console.WriteLine(e.Date.ToString());
        }

    }
}
