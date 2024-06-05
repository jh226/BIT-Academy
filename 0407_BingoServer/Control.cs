using System;
using System.Collections.Generic;
using System.Net.Sockets;
using WB.Net;

namespace _0406_Server
{
    internal class Control
    {
        int Person = 0;
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

       
        public void GameStart(Socket sock, string name)
        {
            bool ret;

            accounts.Add(new Account(name));
            Console.WriteLine("계좌 생성");
            if (Person < 1)
            {
                Person++;
                ret = false;
                string pack = Packet.Game_Pause_Ack(ret, name);
                server.SendData(sock, pack, pack.Length);
            }
            else if(Person ==  1)
            {
                Person++;
                ret = true;
                string pack = Packet.Game_Start_Ack(ret, name);
                server.SendAllData(sock, pack, pack.Length);
            }
            else
            {
                ret = false;
                string pack = Packet.Game_Pause_Ack(ret, name);
                server.SendData(sock, pack, pack.Length);
            }

                

            //2. 응답패킷 생성 및 전송
            

        }

        public void Number(Socket sock, string name , int num)
        {
            string pack = Packet.Game_Num(name, num);
            server.SendAllData(sock, pack, pack.Length);
        }
       
        
    }
}
