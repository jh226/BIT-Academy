using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ServiceModel;
using System.ServiceModel.Description;

namespace _0502_WCFService1
{
    //3. exe기반 호스팅!
    internal class Program
    {
        //wsdl 문서 획득 + http 바인딩 주소
        static string http_url = "http://10.101.15.108/wcf/example/helloworldservice";

        //tcp 바인딩 주소
        static string tcp_url = "net.tcp://localhost:8888/wcf/example/helloworldservice";

        //Http 바인딩(종점 1개 설정)
        static void Hosting1()
        {
            //1. Host객체 생성(서비스 객체, 주소)
            //주소 -> WSDL문서를 획득하는 주소
            ServiceHost host = new ServiceHost(typeof(HelloWorldWCFService),
                                                new Uri(http_url));

            //2. EndPoint 구성
            //주소가 비어 있으면 Host객체 생성시 사용한 주소로 처리됨
            host.AddServiceEndpoint(
                typeof(IHelloWorld),        // contract
                new BasicHttpBinding(),     // binding ( XML WebService )
                "");                        // address
           
            // ServiceMetadataBehavior 설정
            // http://localhost/wcf/example/helloworldservice?wsdl  WSDL문서 획득
            ServiceMetadataBehavior behavior = host.Description.Behaviors.Find<ServiceMetadataBehavior>();
            if (behavior == null)
            {
                behavior = new ServiceMetadataBehavior();
                host.Description.Behaviors.Add(behavior);
            }
            behavior.HttpGetEnabled = true;
            

            //3. Hosting.
            host.Open();
            Console.WriteLine("Press Any key to stop the service");
            Console.ReadKey(true);
            host.Close();
        }

        //Http & Tcp바인딩(종점 2개 설정)
        static void Hosting2()
        {
            ServiceHost host = new ServiceHost(typeof(HelloWorldWCFService),
                    new Uri(http_url),
                    new Uri(tcp_url));
            
			host.AddServiceEndpoint(
                typeof(IHelloWorld),		// contract  
                new BasicHttpBinding(),     // binding
                "");                        // address
            
            host.AddServiceEndpoint(
                            typeof(IHelloWorld),        // contract
                            new NetTcpBinding(),        // binding
                            "");                        // address

            host.Open();
            Console.WriteLine("Press Any key to stop the service");
            Console.ReadKey(true);
            host.Close();
        }

        //config 파일 사용 예
        static void Hosting3()
        {
            ServiceHost host = new ServiceHost(typeof(_0502_WCFService1.HelloWorldWCFService));

            host.Open();
            Console.WriteLine("Press Any key to stop the service");
            Console.ReadKey(true);
            host.Close();
        }
        static void Main(string[] args)
        {
            Hosting3();
        }
    }
}
