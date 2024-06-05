using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _0404_실습.Event
{
    public class DepositEventArgs
    {
        public int Id { get; private set; }
        public double Input { get; private set; }

        public DateTime Date { get; private set; }

        public DepositEventArgs(int id, double input)
        {
            Id = id;
            Input = input;
            Date = DateTime.Now;  //현재 날짜 및 시간
        }
    }
    public delegate void DepositEvent(object obj, DepositEventArgs e);
}
