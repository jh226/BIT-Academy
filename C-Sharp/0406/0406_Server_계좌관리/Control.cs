using System;
using System.Collections.Generic;
using System.Net.Sockets;
using WB.Net;

namespace _0406_Server
{
    internal class Control
    {
        #region 싱글톤

        private Control() { }
        public static Control Instance { get; private set; }
        static Control()
        {
            Instance = new Control();
        }
        #endregion

        private List<Account> accounts = new List<Account>();
        private WbServer1 server;

        public void SendServer(WbServer1 s)
        {
            server = s;
        }

        public void MakeAccount(Socket sock, int id, string name, int balance)
        {
            //1. 수신 데이터 처리
            accounts.Add(new Account(id, name, balance));  
            Console.WriteLine("계좌 생성");

            //2. 응답패킷 생성 및 전송
            string pack = Packet.MakeAccount_Ack(true, id);
            server.SendData(sock, pack, pack.Length);
        }
    
        public void DepositAccount(Socket sock, int id, int money)
        {
            //1. 수신 데이터 처리
            bool result;
            Account acc = accounts.Find( acc1 => acc1.Id == id);
            if (acc == null)
            {
                result = false;
            }
            else
            {
                acc.AddBalance(money);
                result = true;
            }

            //2. 응답패킷 생성 및 전송
            string pack = Packet.DepositAccount_Ack(result, id, acc.Balance);
            server.SendData(sock, pack, pack.Length);
        }
    }
}
