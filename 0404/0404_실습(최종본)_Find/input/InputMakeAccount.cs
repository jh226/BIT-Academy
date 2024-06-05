using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _0404_실습.input
{
    internal class InputMakeAccount
    {
        public string Name { get; private set; }
        public int Id { get; private set; }
        public int Balance { get; private set; }

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
                Id = WbIOLib.InputInteger("계좌번호를 입력하세요");
                //if (CheckId(Id))
                    break;
            }

            while (true)
            {
                Balance = WbIOLib.InputInteger("금액을 입력하세요");
                if (CheckBalance(Balance))
                    break;
            }
        }

        private bool CheckName(string name)
        {
            return (name.Length != 0);
        }
                
        private bool CheckBalance(int balance)
        {
            return (balance > 0);
        }
    }
}
