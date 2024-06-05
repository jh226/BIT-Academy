using System;
using System.Collections.Generic;

namespace _0404_실습
{
    public class Member
    {        
        public string Name      { get; private set; }
        public string Address   { get; set; }

        #region  생성자 
        public Member(string name, string address)
        {
            Name    = name;
            Address = address;
        }
        #endregion
              
        #region object클래스 Override

        public override string ToString()
        {
            return Name + "\t" + Address;
        }
        #endregion 
    
        public void Print()
        {
            Console.WriteLine("이름 : " + Name);
            Console.WriteLine("주소 : " + Address);
        }


    }
}

