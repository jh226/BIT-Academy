using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _0404_실습.manger
{
    public class DeleteEventArgs
    {
        public int Id { get; private set; }

        public DateTime Date { get; private set; }

        public DeleteEventArgs(int id)
        {
            Id = id;
            Date = DateTime.Now;  //현재 날짜 및 시간
        }
    }
    public delegate void DeleteEvent(object obj, DeleteEventArgs e);
}
