using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;
using System.Threading;

namespace _0406
{
    internal class WbServer
    {
        private Socket server;
        

        public int Server_Port { get; private set; }

        public WbServer(int port)
        {
            Server_Port = port;
            Init();
        }

        //Socket -> bind -> listen
        private void Init()
        {
            try
            {
                server = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

                IPEndPoint ipep = new IPEndPoint(IPAddress.Any, Server_Port);
                server.Bind(ipep);
                server.Listen(20);

                Console.WriteLine("서버 시작... 클라이언트 접속 대기중...");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
               Environment.Exit(0);
            }
           
        }

        public void Run()
        {
            
            while(true)
            {

                try
                {
                    Socket client = server.Accept();  
                                                      
                    IPEndPoint ip = (IPEndPoint)client.RemoteEndPoint; 
                    Console.WriteLine("{0}주소, {1}포트 접속", ip.Address, ip.Port);

                    Thread thread = new Thread(WorkThread);
                    thread.IsBackground = true;
                    thread.Start(client);
                    
                }
                catch (Exception ex) 
                {
                    Console.WriteLine(ex.ToString());   
                }
            }
            //server.Close();
        }

        public void WorkThread(object value)
        {
            Socket client = (Socket)value;

            try
            {                
                byte[] data = new byte[1024];

                while (true)
                {
                    //문자열 수신
                    if (client.Receive(data) != 0)
                    {
                        string msg = Encoding.Default.GetString(data).Trim('\0');
                        Console.WriteLine("수신 메시지: " + msg);

                    }
                        
                    else
                        Console.WriteLine("수신 데이터 없음...");

                    //문자열 전송
                    int size = client.Send(data, data.Length, SocketFlags.None);
                    Console.WriteLine("데이터 전송 : {0}byte\n", size);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                client.Close();
            }
        }

    }
}
