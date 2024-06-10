using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _0404_실습.manger
{
    internal class InputSelect
    {
        public int Id { get; private set; }

        public void Select()
        {
            while (true)
            {
                Id = WbIOLib.InputInteger("계좌번호를 입력하세요");
                if (CheckId(Id))
                    break;
            }
        }

        private bool CheckId(int Id)
        {
            return (Id != null);
        }
    }
}
