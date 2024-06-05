using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _0404_실습
{
    public class SelectMemberEventArgs : EventArgs
    {
        public string Name { get; private set; }

        public DateTime Date { get; private set; }

        public SelectMemberEventArgs(string name)
        {
            Name = name;
            Date = DateTime.Now;  //현재 날짜 및 시간
        }
    }

    public delegate void SelectMemberEvent(object obj, SelectMemberEventArgs e);
}
