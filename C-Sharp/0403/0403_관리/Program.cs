using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _0403_관리
{
    class Member
    {

		private static int s_number;
		private int number;     //회원 번호
		private string name;    //이름( 중복되지 않는다.) <---
		private string phone;   //전화번호 <--
		private int age;        //나이 <--
		private string gender;

		public Member(string _name, string _phone, int _age, string _gender)
        {
			name = _name;
			phone = _phone;
			age = _age;
			gender = _gender;
			number = s_number;
			s_number = s_number + 1;
		}

		///get & set 메서드 ( 필요에 의해서 )
		public int getnumber() { return number; }
		public string getname() { return name; }
		public string getphone() { return phone; }
		public int getage() { return age; }
		public string getgender() { return gender; }

		//void setname(string value)	{ name = value;		}
		public void setphone(string value) { phone = value; }
		public void setage(int value) { age = value; }
		//void setgender(char value)	{ gender = value;	}

		///기능 메서드
		public void print()
		{
			Console.WriteLine("[" + number + "] ");
			Console.WriteLine(name + "\t");          //string타입에서 저장된 문자열 반환
			Console.WriteLine(phone + "\t");
			Console.WriteLine(age + "\t");
			Console.WriteLine((string.Equals(gender, 'm') ? "남" : "여"));
		}
		public void println()
		{
			Console.WriteLine("회원번호 : " + number);
			Console.WriteLine("이    름 : " + name);
			Console.WriteLine("전화번호 : " + phone);
			Console.WriteLine("나    이 : " + age);
			Console.WriteLine("성    별 : " + (string.Equals(gender, 'm') ? "남" : "여"));
        }
    }
	class Control
    {
		static int DATA_MAX = 10;
		Member[] members = new Member[DATA_MAX];
		public Control()
		{
			for (int i = 0; i < DATA_MAX; i++)
			{
				members[i] = null;
			}
		}

		public void insert()
		{
			Console.WriteLine("[회원 정보 저장]\n");

			int idx, age;
			string name, phone;
			string gender; //char gender;

			Console.WriteLine("저장할 인덱스(0~" + (DATA_MAX - 1) + ")");
			idx = int.Parse(Console.ReadLine());

			if (members[idx] != null)
			{
				Console.WriteLine("데이터가 존재하는 인덱스 입니다");
				return;
			}

			name = wblib.getstring("이름");
			phone = wblib.getstring("전화번호");
			age = wblib.getint("나이");
			gender = wblib.getstring("성별(m/f)");

			Member pmem = new Member(name, phone, age, gender);
			members[idx] = pmem;
			Console.WriteLine("저장되었습니다\n");
		}
		public void select()
		{
			Console.WriteLine("[회원 정보 검색]\n");
			string name = wblib.getstring("검색할 이름");

			int idx = nametoidx(name);
			if (idx == -1)
			{
				Console.WriteLine("해당 이름은 존재하지 않습니다.");
				return;
			}

			Member pmem = members[idx];
			pmem.println();
		}
		public void update()
		{
			Console.WriteLine("[회원 정보 수정]\n");

			string name = wblib.getstring("검색할 이름");

			int idx = nametoidx(name);
			if (idx == -1)
			{
				Console.WriteLine("해당 이름은 존재하지 않습니다.");
				return;
			}

			//수정작업
			Member pmem = members[idx];

			string phone = wblib.getstring("전화번호");
			int age = wblib.getint("나이");

			pmem.setphone(phone);
			pmem.setage(age);

			Console.WriteLine("수정되었습니다\n");

		}
		public void delete1()
		{
			Console.WriteLine("[회원 정보 삭제]\n");

			string name = wblib.getstring("삭제할 이름");

			int idx = nametoidx(name);
			if (idx == -1)
			{
				Console.WriteLine("해당 이름은 존재하지 않습니다.");
				return;
			}

			//삭제작업
			Member pmem = members[idx];

			//delete(pmem);
			members[idx] = null;

			Console.WriteLine("삭제되었습니다");
		}

		public void printall()
		{
			for (int i = 0; i < DATA_MAX; i++)
			{
				if (members[i] != null)
				{
					Console.WriteLine(i + " : ");
					members[i].print();
				}
			}
		}

		int nametoidx(string name)
		{
			for (int i = 0; i < DATA_MAX; i++)
			{
				//주소를 갖고 있느냐?
				//주소를 가지고 있다면 그 이름이 name이 맞느냐?
				if (members[i] != null && members[i].getname() == name)
				{
					return i;
				}
			}
			return -1;
        }
    }

	class wblib
    {
		public static int getint(string msg)
		{
			int num;
			Console.WriteLine(msg + " : ");
			num = Console.Read();
			return num;
		}
		public static string getstring(string msg)
		{
			string str;
			Console.WriteLine(msg + " : ");     // cout >> : 내부적으로 printf() 를 호출!
			str = Console.ReadLine();           // cin >> : 내부적으로 scanf_s()  를 호출!
			Console.ReadLine();                 //cin.getline(str, sizeof(str));  //, gets_s()
			return str;
		}
	}


    class Program
    {
		static int DATA_MAX = 10;
		static Member []members;

		static void init()
		{
			Console.Clear();
			Console.WriteLine("***************************************************************");
			Console.WriteLine(" 비트 고급 37기\n");
			Console.WriteLine(" OOP 기반의 회원 관리 프로그램\n");
			Console.WriteLine(" 2023.04.03\n");
			Console.WriteLine(" 이지현\n");
			Console.WriteLine("***************************************************************");
			Console.ReadLine();
		}
		static void run()
		{
			Control con = new Control();
			while (true)
			{
				Console.Clear();
				con.printall();
				switch (menuprint())
				{
					case "0": return;
					case "1": con.insert(); break;
					case "2": con.select(); break;
					case "3": con.update(); break;
					case "4": con.delete1(); break;
				}
				Console.ReadLine();
			}
		}


		static string menuprint()
		{
			Console.WriteLine("***************************************************************\n");
			Console.WriteLine("[0] 프로그램 종료\n");
			Console.WriteLine("[1] Insert(회원 정보 저장)\n");
			Console.WriteLine("[2] Select(회원 정보 검색 - 이름으로 검색, 이름은 uniq)\n");
			Console.WriteLine("[3] Update(회원 정보 수정 - 이름으로 검색해서 전화번호와 나이를 수정)\n");
			Console.WriteLine("[4] Delete(회원 정보 삭제 - 이름으로 검색해서 해당 정보를 0으로 초기화)\n");
			Console.WriteLine("***************************************************************\n");
			return Console.ReadLine();
		}
		
		static void ending()
		{
			Console.Clear();
			Console.WriteLine("***************************************************************\n");
			Console.WriteLine(" 프로그램을 종료합니다.\n");
			Console.WriteLine("***************************************************************\n");
			//system("pause"); //멈춤
		}


		static void Main(string[] args)
        {
            init();
			run();
			ending();
		}
    }
}
