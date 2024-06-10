using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _0404_실습.manger
{
    public class DeleteMemberEventArgs
    {
        public string Name { get; private set; }

        public DateTime Date { get; private set; }

        public DeleteMemberEventArgs(string name)
        {
            Name = name;
            Date = DateTime.Now;  //현재 날짜 및 시간
        }
    }

    public delegate void DeleteMemberEvent(object obj, DeleteMemberEventArgs e);

}

