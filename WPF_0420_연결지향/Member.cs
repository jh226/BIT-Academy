using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _0418_DB
{
    internal class Member
    {
        public string Name { get; set; }
        public string Phone { get; set; }
        public int Age { get; set;  }
        public DateTime NetDateTime { get; set; }

        public Member(string name, string phone, int age, DateTime dt)
        {
            Name = name;
            Phone = phone;
            Age = age;
            NetDateTime = dt;
        }

        public override string ToString()
        {
            return Name + ", " + Phone + ", " + Age + ", " + NetDateTime;
        }
    }
}
