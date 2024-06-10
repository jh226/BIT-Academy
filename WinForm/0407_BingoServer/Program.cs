using System;
using System.Net.Sockets;
using WB.Net;

namespace _0406_Server
{
    internal class Program
    {
        private WbServer1 server = new WbServer1(7000);

        public Program()
        {
            Control.Instance.SendServer(server);
        }

        public void Server_RecvData(Socket sock, string msg)
        {
            Console.WriteLine("수신 메시지: " + msg);
            string[] sp1 = msg.Split('@');
            
           
            if(sp1[0].Equals(Packet.GAME_START))
            {
                string[] sp2 = sp1[1].Split('#');
                Control.Instance.GameStart(sock,
                    sp2[0]);
            }
            else if(sp1[0].Equals(Packet.GAME_NUM))
            {
                string[] sp2 = sp1[1].Split('#');
                Control.Instance.Number(sock, sp2[0], int.Parse(sp2[1]));

                 
            }

        }

        public void Run()
        {
            server.Run(Server_RecvData);
        }

        static void Main(string[] args)
        {
            new Program().Run();
        }
    }
}
