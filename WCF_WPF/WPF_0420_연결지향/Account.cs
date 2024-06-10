using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _0418_DB
{
    internal class Account
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Balance { get; set; }
        public DateTime NetDateTime { get; set; }

        public Account(int id, string name, int balance, DateTime dt)
        {
            Id = id;
            Name = name;
            Balance = balance;
            NetDateTime = dt;
        }
        public override string ToString()
        {
            return Id + ", " + Name + ", " + Balance + ", " + NetDateTime;
        }

    }
}
