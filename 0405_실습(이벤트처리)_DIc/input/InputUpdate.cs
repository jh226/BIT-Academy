using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _0404_실습.input
{
    internal class InputUpdate
    {
        public string Name { get; private set; }
        public string Addr { get; private set; }

        public void Update()
        {
            while (true)
            {
                Name = WbIOLib.InputString("이름을 입력하세요");    
                if (CheckName(Name))
                    break;
            }
            Addr = WbIOLib.InputString("수정할 주소를 입력하세요");
        }

        private bool CheckName(string name)
        {
            return (name.Length != 0);
        }
    }
}
