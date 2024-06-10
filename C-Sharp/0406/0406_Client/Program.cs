using System;
using _0404_실습;

namespace _0406_Client
{
    internal class Program
    {
        static void Main(string[] args)
        {
            App app = App.Instance;
            app.Init();
            app.Run();
            app.Exit();
        }
    }
}
