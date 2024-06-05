using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace _0404_실습.input
{
    public class SelectEventArgs
    {
        public int Id { get; private set; }
        public DateTime Date { get; private set; }

        public SelectEventArgs(int id)
        {
            Id = id;
            Date = DateTime.Now;  //현재 날짜 및 시간
        }
    }
    public delegate void SelectEvent(object obj, SelectEventArgs e);
}
