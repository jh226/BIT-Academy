using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _0404_실습.Lib
{
    public class MakeAccountEventArgs : EventArgs
    {
        public int Id { get; private set; }
        public string Name { get; private set; }
        public double Balance { get; private set; }

        public DateTime Date { get; private set; }

        public MakeAccountEventArgs(Account account)
        {
            Id = account.Id;
            Name = account.Name;
            Balance = account.Balance;
            
            Date = DateTime.Now;  //현재 날짜 및 시간
        }
    }
    public delegate void MakeAccountEvent(object obj, MakeAccountEventArgs e);
}
