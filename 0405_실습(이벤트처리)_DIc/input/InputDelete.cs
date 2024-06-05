using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _0404_실습.manger
{
    internal class InputDelete
    {
        public string Name { get; private set; }

        public void Delete()
        {
            while (true)
            {
                Name = WbIOLib.InputString("이름을 입력하세요");
                if (CheckName(Name))
                    break;
            }
        }

        private bool CheckName(string name)
        {
            return (name.Length != 0);
        }
    }
}
