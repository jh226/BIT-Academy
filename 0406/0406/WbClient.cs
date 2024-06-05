using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace _0406
{
    internal delegate void Recv_Del1(string msg);

    internal class WbClient
    {
        private Socket sock;
        private Recv_Del1 recv_Del = null;

        public Thread RThread { get; private set; }

        public string Ip { get; private set; }

        public int Port { get; private set; }

        public WbClient(string ip, int port)
        {
            Ip = ip;
            Port = port;
        }

        #region 외부 접근 허용 메서드
        public bool Open(Recv_Del1 fun)
        {
            recv_Del = fun;
            try
            {
                //1) 소켓생성
                sock = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

                //2) 연결
                IPEndPoint ipep = new IPEndPoint(IPAddress.Parse(Ip), Port);
                sock.Connect(ipep);

                //3) 수신 스레드 생성
                RThread = new Thread(RecvThread);
                RThread.IsBackground = true;
                RThread.Start();

                //4) 결과 반환
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }
        }

        public void Close()
        {
            sock.Close();
            RThread.Abort();     //스레드 강제 종료
        }
        #endregion

        #region 내부 메서드
        public void RecvThread()
        {
            byte[] data = new byte[1024];

            while (true)
            {
                // sock.Receive(data);
                ReceiveData(sock, ref data);
                string msg = Encoding.Default.GetString(data).Trim('\0');
                
                recv_Del(msg);
            }
        }
        
        public void SendData(string msg, int size) 
        {
            byte[] buffer = Encoding.Default.GetBytes(msg);
           // sock.Send(buffer);
            SendData(sock, buffer, size);
        }
        #endregion

        #region 데이터 송수신
        private void SendData(Socket sock, byte[] data, int _size)
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

        private bool ReceiveData(Socket sock, ref byte[] data)
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
        
        #endregion
    }
}
