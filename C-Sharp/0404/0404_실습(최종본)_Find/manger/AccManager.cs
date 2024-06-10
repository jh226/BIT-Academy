using _0404_실습.Event;
using _0404_실습.input;
using _0404_실습.Lib;
using _0404_실습.manger;
using System;
using System.Collections.Generic;
using System.Linq;

namespace _0404_실습
{
    internal class AccManager
    {
        #region 싱글톤
        public static AccManager Instance { get; private set; }
        private AccManager()
        {

        }
        static AccManager()
        {
            Instance = new AccManager();
        }
        #endregion

        #region 이벤트
        public event MakeAccountEvent MakeAccountEventHandler = null;
        public event DepositEvent DepositEventHandler = null;
        public event WithdrawEvent WithdrawEventHandler = null;
        public event SelectEvent SelectEventHandler = null;
        public event DeleteEvent DeleteEventHandler = null;
        #endregion

        private Dictionary<int, Account> accounts = new Dictionary<int, Account>();

        #region 기능

        public void PrintAll()
        {
            foreach(Account account in accounts.Values)
            {
                Console.WriteLine(account.ToString());
            }
        }
       
        public void MakeAccount()
        {
            try
            {
                Console.WriteLine("\n***** 계좌 종류 *****");
                Console.WriteLine("[1] 일반 계좌");
                Console.WriteLine("[2] 신용 계좌");
                Console.WriteLine("[3] 기부 계좌");

                int select = WbIOLib.InputInteger("선택할 기능", 1, 3);

                Console.WriteLine("***** 계좌 개설 *****");
                string name = WbIOLib.InputString("성함");
                int id = WbIOLib.InputInteger("계좌번호");
                int balance = WbIOLib.InputInteger("입금금액");
                Console.WriteLine("\n");

                Account acc;

                if (select == 1)    //일반 계좌
                {
                    acc = new Account(id, name, balance);
                    accounts.Add(id, acc);
                    MakeAccountEventHandler?.Invoke(this, new MakeAccountEventArgs(acc));
                }    
                else if (select == 2)    //신용 계좌
                {
                    acc = new FaithAccount(id, name, balance);
                    accounts.Add(id, acc);
                    MakeAccountEventHandler?.Invoke(this, new MakeAccountEventArgs(acc));
                }
                else if (select == 3)    //기부 계좌
                {
                    acc = new ContriAccount(id, name, balance);
                    accounts.Add(id, acc);
                    MakeAccountEventHandler?.Invoke(this, new MakeAccountEventArgs(acc));
                }
                Console.WriteLine("계좌 생성");              
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                Console.WriteLine("계좌 생성 실패");
            }
        }
        
        public void Deposit()
        {
            try
            {
                Console.WriteLine();
                InputDeposit deposit = new InputDeposit();
                deposit.Deposit();

                Account acc = accounts[deposit.Id];

                acc.AddBalance(deposit.Input);
                DepositEventHandler?.Invoke(this, new DepositEventArgs(deposit.Id, deposit.Input));

            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
               
        public void Withdraw()
        {
            try
            {
                Console.WriteLine();
                InputWithdraw withdraw = new InputWithdraw();
                withdraw.Withdraw();

                Account acc = accounts[withdraw.Id];
                acc.MinBalance(withdraw.output);
                WithdrawEventHandler?.Invoke(this, new WithdrawEventArgs(withdraw.Id, withdraw.output)); 
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
        
        public void Inquire()
        {
            try
            {
                Console.WriteLine();
                InputSelect select = new InputSelect();
                select.Select();

                Account acc = accounts[select.Id];
                acc.ShowAllData();
                SelectEventHandler?.Invoke(this, new SelectEventArgs(select.Id));
               
            }
            catch(Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }

        public void DeleteAccount()
        {
            try
            {
                Console.WriteLine();
                InputDelete delete = new InputDelete();
                delete.Delete();

                accounts.Remove(delete.Id);
                DeleteEventHandler?.Invoke(this, new DeleteEventArgs(delete.Id));
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }

        public void Sort_AccID()
        {
            accounts = Sort_Key(accounts);  //ID 오름차순
            
        }

        public static Dictionary<int, Account> Sort_Key(Dictionary<int, Account> dict)
        {
            Dictionary<int, Account> sortDict = new Dictionary<int, Account>();

            List<int> list = dict.Keys.ToList();

            list.Sort();

            foreach (var key in list)
            {
                sortDict.Add(key, dict[key]);
            }

            return sortDict;
        }

        public Dictionary<int, Account> Sort_Balacne(Dictionary<int, Account> dict)
        {
            //Dictionary<int, Account> sortDict = new Dictionary<int, Account>();

            //foreach (var values in dict.Values)
            //{
            //    double balarr = values.Balance;
            //    List<double> list.key = balarr;
            //}
            


            //list.Sort();

            //foreach (var key in list)
            //{
            //    sortDict.Add(key, dict[key]);
            //}

            //return sortDict;
        }

        #endregion


    }
}
