using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace _0503_chat
{
    internal class Program
    {
        static void Main(string[] args)
        {
            //Address
            Uri uri = new Uri(ConfigurationManager.AppSettings["addr"]);
            //Contract-> Setting
            //Binding -> App.Config
            ServiceHost host = new ServiceHost(typeof(_0503_chat.ChatService), uri);

            //오픈
            host.Open();
            Console.WriteLine("채팅 서비스를 시작합니다. {0}", uri.ToString());
            Console.WriteLine("http://10.101.36.76:9000/GetService");
            Console.WriteLine("멈추시려면 엔터를 눌러주세요..");
            Console.ReadLine();

            

            //서비스
            host.Abort();
            host.Close();

        }
    }
}
