using _0404_실습.manger;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static _0404_실습.manger.UpdateMemberEventArgs;

namespace _0404_실습
{
    internal class MemberLog
    {
        public MemberLog()
        {
            MemberManager mm = MemberManager.Instance;
            mm.AddMemberEventHandler += new AddMemberEvent(OnAddMemberEventHandler);
            mm.SelectMemberEventHandler += new SelectMemberEvent(OnSelectMemberEventHandler);
            mm.UpdateMemberEventHandler += new UpdateMemberEvent(OnUpdateMemberEventHandler);
            mm.DeleteMemberEventHandler += new DeleteMemberEvent(OnDeleteMemberEventHandler);
        }

        public void OnAddMemberEventHandler(object obj, AddMemberEventArgs e)
        {
            Console.WriteLine("[회원가입로그]");
            Console.WriteLine(e.Name + ", " + e.Addr);
            Console.WriteLine(e.Date.ToString());
        }

        public void OnSelectMemberEventHandler(object obj, SelectMemberEventArgs e)
        {
            Console.WriteLine("[회원검색로그]");
            Console.WriteLine(e.Name);
            Console.WriteLine(e.Date.ToString());
        }

        public void OnUpdateMemberEventHandler(object obj, UpdateMemberEventArgs e)
        {
            Console.WriteLine("[회원수정로그]");
            Console.WriteLine(e.Name);
            Console.WriteLine(e.Date.ToString());
        }
        public void OnDeleteMemberEventHandler(object obj, DeleteMemberEventArgs e)
        {
            Console.WriteLine("[회원삭제로그]");
            Console.WriteLine(e.Name);
            Console.WriteLine(e.Date.ToString());
        }
    }
}
