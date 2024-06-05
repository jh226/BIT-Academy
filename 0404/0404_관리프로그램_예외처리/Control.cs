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

		public void insert(int idx, string name, string phone, int age, char gender)
		{
			try
			{
				Console.WriteLine("[회원 정보 저장]");

				idx_check(idx);
				Member member = new Member(name, phone, age, gender);
				members[idx] = member;
				Console.WriteLine("저장되었습니다");
			}
			catch (Exception ex)
            {
				Console.WriteLine(ex.Message);
            }
		}

		/// <summary>
		/// 인덱스 사용여부 체크
		/// 사용할 수 없으면 Exception 예외를 발생시킨다.
		/// </summary>
		/// <param name="idx"></param>
		private void idx_check(int idx)
        {
			if (members[idx] != null)
			{
				throw new Exception("이미 데이터가 존재합니다.");
			}
		}

		public void select(string name) 
		{
			Console.WriteLine("[회원 정보 검색]");

			try
			{
				int idx = nametoidx(name);
				Member member = members[idx];
				member.println();
			}
			catch (Exception e)
            {
				Console.WriteLine(e.Message);
            }
		}

		public void update(string name, string phone, int age)
		{
			try
			{
				Console.WriteLine("[회원 정보 수정]\n");

				int idx = nametoidx(name);
				Member member = members[idx];
				member.Phone = phone;
				member.Age = age;

				Console.WriteLine("수정되었습니다");
			}
			catch(Exception e)
            {
				Console.WriteLine (e.Message);
            }
		}

		public void delete1(string name)
		{
			try
			{
				Console.WriteLine("[회원 정보 삭제]");

				int idx = nametoidx(name);
				//삭제작업
				members[idx] = null;        //*****************
				Console.WriteLine("삭제되었습니다");
			}
			catch(Exception ex)
            {
				Console.WriteLine(ex.Message);
            }
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
			throw new Exception("해당 이름을 찾을 수 없습니다.");
		}
     
		#endregion 
    }
}