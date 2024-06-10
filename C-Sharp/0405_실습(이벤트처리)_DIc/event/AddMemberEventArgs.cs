using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _0404_실습
{
    public class AddMemberEventArgs : EventArgs
    {
        public string Name { get; private set; }
        public string Addr { get; private set; }

        public DateTime Date { get; private set; }

        public AddMemberEventArgs(Member member)
        {
            Name = member.Name;
            Addr = member.Address;
            Date = DateTime.Now;  //현재 날짜 및 시간
        }
    }

    public delegate void AddMemberEvent(object obj, AddMemberEventArgs e);
}
