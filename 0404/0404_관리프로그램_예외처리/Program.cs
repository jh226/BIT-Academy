using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _0404_관리프로그램
{
    internal class Program
    {
        static void Main(string[] args)
        {
            //App app = new App();
            //App app1 = new App();
            App app = App.Singleton;
            app.init();
            app.run();
            app.exit();
        }
    }
}
