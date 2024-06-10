using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _0404_실습.input
{
    internal class InputWithdraw
    {
        public int Id { get; private set; }
        public double output { get; private set; }

        public void Withdraw()
        {
            while (true)
            {
                Id = WbIOLib.InputInteger("계좌번호를 입력하세요");
                if (CheckId(Id))
                    break;
            }
            output = WbIOLib.InputDouble("출금액을 입력하세요");
        }

        private bool CheckId(int id)
        {
            return (id != null);
        }
    }
}
