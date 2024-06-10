using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Configuration;
using System.Text;
using System.Threading.Tasks;

//다중 접속 1대 1서버 - 에코

namespace _0406
{
    internal class Program
    {

        #region 서버 사용 

        static void Server_RecvData(string msg)
        {
            Console.WriteLine("수신 메시지: " + msg);
        }

        static void Server_Sample()
        {
            WbServer1 server = new WbServer1(7000);
            server.Run(Server_RecvData);
        }
        #endregion

        #region 클라이언트 사용 
        static void Client_RecvData(string msg)
        {
            Console.WriteLine("수신 메시지: " + msg);
        }

        static void Client_Sample()
        {
            WbClient client = new WbClient("10.101.15.108",7000);

            if(client.Open(Client_RecvData) == false)
                return;
            while(true) 
            {
                string msg = Console.ReadLine();
                if(msg == string.Empty)
                        break;

                client.SendData(msg, msg.Length);
            }
            client.Close();
        }
        #endregion

        static void Main(string[] args)
        {
            //Server_Sample();
            Client_Sample();           
        }
    }
}
