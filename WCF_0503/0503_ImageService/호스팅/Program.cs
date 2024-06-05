using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace _0503_ImageService
{
    internal class Program
    {
        static void Main(string[] args)
        {
            ServiceHost host = new ServiceHost(typeof(_0503_ImageService.ImageControl));

            host.Open();
            Console.WriteLine("Press Any key to stop the service");
            Console.ReadKey(true);
            host.Close();
        }
    }
}
