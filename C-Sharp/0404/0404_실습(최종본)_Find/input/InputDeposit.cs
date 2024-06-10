using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _0404_실습.manger
{
    internal class InputDeposit
    {
        public int Id { get; private set; }
        public double Input { get; private set; }

        public void Deposit()
        {
            while (true)
            {
                Id = WbIOLib.InputInteger("계좌번호를 입력하세요");
                if (CheckId(Id))
                    break;
            }
            Input = WbIOLib.InputDouble("입금액을 입력하세요");
        }

        private bool CheckId(int id)
        {
            return (id != null);
        }
    }
}
