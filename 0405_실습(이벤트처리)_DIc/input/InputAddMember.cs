using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _0404_실습
{
    internal class InputAddMember
    {
        public string Name { get; private set; }
        public string Addr { get; private set; }
       
        public void Input()
        {
            while (true)
            {
                Name = WbIOLib.InputString("이름을 입력하세요");
                if (CheckName(Name))
                    break;
            }

            while (true)
            {
                Addr = WbIOLib.InputString("주소를 입력하세요");
                if (CheckAdd(Addr))
                    break;
            }
        }

        private bool CheckName(string name)
        {
            return (name.Length != 0);
        }

        private bool CheckAdd(string arr)
        {
            return (arr.Length != 0);
        }
    }
}
