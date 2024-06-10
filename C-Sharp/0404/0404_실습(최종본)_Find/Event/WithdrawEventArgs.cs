using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _0404_실습.Event
{
    public class WithdrawEventArgs
    {
        public int Id { get; private set; }
        public double Output { get; private set; }

        public DateTime Date { get; private set; }

        public WithdrawEventArgs(int id, double output)
        {
            Id = id;
            Output = output;
            Date = DateTime.Now;  //현재 날짜 및 시간
        }
    }
    public delegate void WithdrawEvent(object obj, WithdrawEventArgs e);
}
