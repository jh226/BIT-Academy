using System;

namespace _0404_관리프로그램
{
    internal class Member
    {
		//맴버 필드
		private	static int s_number = 1000;
		private int number;     //회원 번호
		private string name;    //이름( 중복되지 않는다.) <---
		private string phone;   //전화번호 <--
		private int age;        //나이 <--
		private char gender;    //성별 <--

        #region 프로퍼티
		public int Number { get { return number; } private set { number = value; } }	
		public string Name { get { return name; }	private set { name = value; } }	
		public string Phone { get { return phone; }  set { phone = value; } }	
		public int Age { get { return age; }		set { age = value; } }	
		public char Gender { get { return gender; } private set { gender = value; } }

        #endregion

        #region 생성자
        public Member(string _name, string _phone, int _age, char _gender)
        {
			Name = _name;
			this.Phone = _phone;
			this.Age = _age;				
			this.Gender = _gender;
			Number = s_number;
			s_number++;
        }
        #endregion 

        #region 메서드
        public void print() 
		{
			Console.Write("[ {0} ]", number);
			Console.Write(name + "\t");
			Console.Write(phone + "\t");
			Console.Write(age + "\t");
			Console.WriteLine(gender);
		}
		
		public void println()
        {
			Console.WriteLine("[ {0} ]", number);
			Console.WriteLine("이름 : " + name);
			Console.WriteLine("전화번호 : " + phone);
			Console.WriteLine("나이 : " + age);
			Console.WriteLine("성별 : " + gender);
		}

		#endregion 
	}
}
