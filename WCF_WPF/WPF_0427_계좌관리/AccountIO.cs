using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace _0427_계좌
{
    internal class AccountIO
    {
        public int? Id { get; set; }
        public int? Input { get; set; }
        public int? Output { get; set; }
        public int Balance { get; set; }
        public DateTime? NewDate { get; set; }

        public AccountIO() { }
        public AccountIO(int id, int input, int output, int balance)
        {
            Id = id;
            Input = input;
            Output = output;
            Balance = balance;
            NewDate = DateTime.Now;
        }

        public override string ToString()
        {
            return Id + ", " + Input + ", "+ Output + ", "+ Balance + ", " + NewDate.ToString();

        }
    }
}
