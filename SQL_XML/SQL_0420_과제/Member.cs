using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _0420_미션
{
    internal class Member
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Phone { get; set; }
        public int Age { get; set; }
        public DateTime Dt { get; set; }

        public Member(int id, string name, int phone, int age, DateTime dt)
        {
            Id = id;
            Name = name;
            Phone = phone;
            Age = age;
            Dt = dt;
        }
    }
}
