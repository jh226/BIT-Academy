using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using wb.example;

namespace _0404_관리프로그램
{
    internal class Control
    {
        //private Member[] members = new Member[10];
        private Member[] members;

        public Control()
        {
            int size = Wblib.getint("저장 개수");
            members = new Member[size];

            for (int i= 0; i< members.Length; i++)
            {
                members[i] = null;
            }
        }

		#region 메서드
		public void printall() 
		{
			for (int i = 0; i< members.Length; i++)
			{
				Member mem = members[i];
				if (mem != null)
				{
					Console.Write("[{0}] ",i);
					mem.print();
				}
			}
		}

		public void insert()
		{
			Console.WriteLine("[회원 정보 저장]");

			string temp = string.Format("저장할 인덱스(0~%d)", members.Length - 1);
			int idx = Wblib.getint(temp);
			if (members[idx] != null)
			{
				Console.WriteLine("데이터가 존재하는 인덱스 입니다");
				return;
			}

			string name		= Wblib.getstring("이름");
			string phone	= Wblib.getstring("전화번호");
			int age			= Wblib.getint("나이");
			char gender		= Wblib.getchar("성별(남/여)");

			Member member = new Member(name, phone, age, gender);
			members[idx] = member;
			Console.WriteLine("저장되었습니다");
		}

		public void select()
		{
			Console.WriteLine("[회원 정보 검색]");

			string name = Wblib.getstring("검색할 이름");

			int idx = nametoidx(name);
			if (idx == -1)
			{
				Console.WriteLine("해당 이름은 존재하지 않습니다.");
				return;
			}

			Member member = members[idx];
			member.println();
		}

		public void update()
		{
			Console.WriteLine("[회원 정보 수정]\n");

			string name = Wblib.getstring("검색할 이름");

			int idx = nametoidx(name);
			if (idx == -1)
			{
				Console.WriteLine("해당 이름은 존재하지 않습니다.");
				return;
			}

			//수정작업
			string phone = Wblib.getstring("전화번호");
			int		age  = Wblib.getint("나이");

			Member member = members[idx];
			member.Phone = phone;
			member.Age = age;

			Console.WriteLine("수정되었습니다");
		}

		public void delete1()
		{
			Console.WriteLine("[회원 정보 삭제]");

			string name = Wblib.getstring("삭제할 이름");

			int idx = nametoidx(name);
			if (idx == -1)
			{
				Console.WriteLine("해당 이름은 존재하지 않습니다.");
				return;
			}

			//삭제작업
			members[idx] = null;		//*****************

			Console.WriteLine("삭제되었습니다");
		}

		private int nametoidx(string name) 
		{
			for (int i = 0; i<members.Length; i++)
			{
				Member member = members[i];
				if (member != null && member.Name == name)
				{
					return i;
				}
			}
			return -1;
		}
     
		#endregion 
    }
}