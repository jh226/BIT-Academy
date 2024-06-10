using System;
using System.Collections.Generic;
using Wb.Net;

namespace _0404_실습
{
    internal class AccManager
    {
        #region 싱글톤
        public static AccManager Instance { get; private set; }
        private AccManager()
        {
            if (client.Open(Client_RecvData) == false)
            {
                Environment.Exit(0);
                return;
            }
        }
        static AccManager()
        {
            Instance = new AccManager();
        }
        #endregion 

        private List<Account> accounts = new List<Account>();
        private WbClient client = new WbClient("10.101.15.108", 7000);

        #region 시작 기능

        public void PrintAll()
        {
            foreach(Account account in accounts)
            {
                Console.WriteLine(account.ToString());
            }
        }
       
        public void MakeAccount()
        {
            try
            {               
                Console.WriteLine("***** 계좌 개설 *****");

                //1. 필요한 정보를 입력
                string name = WbIOLib.InputString("성함");
                int id = WbIOLib.InputInteger("계좌번호");
                int balance = WbIOLib.InputInteger("입금금액");
                Console.WriteLine("\n");

                //2. 패킷 구성
                string packet = Packet.MakeAccount(id, name, balance);

                //3. 전송
                client.SendData(packet, packet.Length);               
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
                Console.WriteLine("\n***** 계좌 입금 *****");

                //1.필요한 정보 입력
                int find = WbIOLib.InputInteger("입금할 계좌번호");
                int add = WbIOLib.InputInteger("입금할 금액");

                //2. 패킷 구성
                string packet = Packet.DepositAccount(find, add);

                //3. 전송
                client.SendData(packet, packet.Length);

                //Account acc = AccIDToAccount(find);

                //Find_AccID fa = new Find_AccID(find);
                //Predicate<Account> predicate = fa.FindAccID;
                //Account acc = accounts.Find(predicate);
                //if (acc == null)
                //    throw new Exception("없는 계좌번호입니다");

                //acc.AddBalance(add);

                //Console.WriteLine("입금 성공");
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
                Console.WriteLine("\n***** 계좌 출금 *****");

                //1.필요한 정보 입력
                int find = WbIOLib.InputInteger("출금할 계좌번호");
                int add = WbIOLib.InputInteger("출금할 금액");

                //2. 패킷 구성
                string packet = Packet.WithdrawAccount(find, add);

                //3. 전송
                client.SendData(packet, packet.Length);


                //Account acc = accounts.Find(new Find_AccID(find).FindAccID);
                //if (acc == null)
                //    throw new Exception("없는 계좌번호입니다");
                ////Account acc = AccIDToAccount(find);


                //acc.MinBalance(add);

                //Console.WriteLine("출금 성공");
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
                Console.WriteLine("\n***** 잔액 조회 *****");

                //1.필요한 정보 입력
                int find = WbIOLib.InputInteger("조회할 계좌번호");

                //2. 패킷 구성
                string packet = Packet.InquireAccount(find);

                //3. 전송
                client.SendData(packet, packet.Length);

                //Account acc = AccIDToAccount(find);
                //Account acc = accounts.Find( acc1 => acc1.Id == find );
                //if (acc == null)
                //    throw new Exception("없는 계좌번호입니다");

                //acc.ShowAllData();
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
                Console.WriteLine("\n***** 계좌 삭제 *****");
                int find = WbIOLib.InputInteger("삭제할 계좌번호");

                Account acc = AccIDToAccount(find);
                accounts.Remove(acc);
                Console.WriteLine("삭제되었습니다");
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }

        public void Sort_AccID()
        {
            accounts.Sort();  //ID 오름차순
        }

        public void Sort_Balacne()
        {
            accounts.Sort(new Sort_Balance());
        }

        #endregion

        #region 검색 기능 - 삭제 예정 -

        private Account AccIDToAccount(int accid)
        {
            foreach(Account account in accounts)
            {
                if(account.Id == accid)
                    return account;
            }
            throw new Exception("없는 계좌번호입니다");
            //return null;
        }

        #endregion

        #region 응답 기능
        private void MakeAccount_Ack(bool result, int id)
        {
            if (result == true)
                Console.WriteLine("{0} 계좌 개설 성공", id);
            else
                Console.WriteLine("{0}계좌 개설 실패", id);
        }


        #endregion 

        #region 네트워크 
        public void Client_RecvData(string msg)
        {
            Console.WriteLine("수신 메시지: " + msg);
            string[] sp1 = msg.Split('@');

            if (sp1[0].Equals(Packet.MAKE_ACCOUNT_ACK))
            {
                string[] sp2 = sp1[1].Split('#');
                MakeAccount_Ack(bool.Parse(sp2[0]), int.Parse(sp2[1]));
            }
        }
        #endregion 
    }
}
