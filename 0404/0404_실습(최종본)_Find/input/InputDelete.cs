using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _0404_실습.manger
{
    internal class InputDelete
    {
        public int Id { get; private set; }

        public void Delete()
        {
            while (true)
            {
                Id = WbIOLib.InputInteger("이름을 입력하세요");
                if (CheckId(Id))
                    break;
            }
        }

        private bool CheckId(int id)
        {
            return (id != 0);
        }
    }
}
