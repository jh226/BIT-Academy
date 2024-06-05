using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _0404_실습
{
    internal class Start
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
