using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _0404_실습.manger
{
    public class UpdateMemberEventArgs
    {
        public string Name { get; private set; }

        public DateTime Date { get; private set; }

        public UpdateMemberEventArgs(string name)
        {
            Name = name;
            Date = DateTime.Now;  //현재 날짜 및 시간
        }
    }
    public delegate void UpdateMemberEvent(object obj, UpdateMemberEventArgs e);
}
