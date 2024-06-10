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
    internal delegate void Recv_Del(string msg);

    internal class WbServer1
    {
        //대기소켓
        private Socket server;
        private Recv_Del recv_Del = null;
        public int Server_Port { get; private set; }

        //통신소켓
        private List<Socket> sockets = new List<Socket>();

        #region 생성자 및 초기화
        
        public WbServer1(int port)
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
        
        #endregion

        #region 엔진부 - 반복
        
        public void Run(Recv_Del fun)
        {
            recv_Del = fun;
            while (true)
            {

                try
                {
                    Socket client = server.Accept();
                    sockets.Add(client);

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
                byte[] data = null;

                while (true)
                {
                    string msg = string.Empty;
                    //문자열 수신
                    if ((ReceiveData(client, ref data)) == true)
                    {
                        msg = Encoding.Default.GetString(data).Trim('\0');
                        recv_Del(msg);
                    }
                    else
                    {
                        Console.WriteLine("수신 데이터 없음...");
                        throw new Exception("수신오류");
                    }
                        

                    //SendData(client, msg, data.Length);
                    SendAllData(client, msg, data.Length);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                sockets.Remove(client);
                client.Close();
            }
        }

        #endregion

        #region 데이터 송신부
        public void SendData(Socket sock, string msg, int size)
        {
            //문자열 전송
            byte[] bmsg = Encoding.Default.GetBytes(msg);
            SendData(sock, bmsg, size);
            //int ret = sock.Send(bmsg, bmsg.Length, SocketFlags.None);
            //Console.WriteLine("데이터 전송 : {0}byte\n", ret);
        }
        public void SendAllData(Socket sock, string msg, int size)
        {
            foreach(Socket s in sockets)
            {
                SendData(s, msg, size);
            }
        }
        #endregion

        public void SendData(Socket sock, byte[] data, int _size)
        {
            try
            {   
                int total = 0;          //보낸 크기
                int size = _size;       //보낼 크기
                int left_data = size;   //남은 크기

                //1) 전송할 데이터의 크기 전달
                byte[] data_size = new byte[4];
                
                data_size = BitConverter.GetBytes(size);
                int ret = sock.Send(data_size);

                //2) 실제 데이터 전송
                while (total < size)
                {
                    ret = sock.Send(data, total, left_data, SocketFlags.None);
                    total += ret;
                    left_data -= ret;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        public bool ReceiveData(Socket sock, ref byte[] data)
        {
            try
            {
                int total = 0;      // 실제 받은 크기
                int size = 0;       // 실제 받을 크기
                int left_data = 0;  // 남은 크기

                // 수신할 데이터 크기 알아내기 
                byte[] data_size = new byte[4];
                int ret = sock.Receive(data_size, 0, 4, SocketFlags.None);
                size = BitConverter.ToInt32(data_size, 0);
                left_data = size;

                data = new byte[size];

                // 실제 데이터 수신
                while (total < size)
                {
                    ret = sock.Receive(data, total, left_data, 0);
                    if (ret == 0) break;
                    total += ret;
                    left_data -= ret;
                }
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }
        }
    }
}
