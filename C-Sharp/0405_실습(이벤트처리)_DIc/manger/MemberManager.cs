using _0404_실습.input;
using _0404_실습.manger;
using System;
using System.Collections.Generic;
using static _0404_실습.manger.UpdateMemberEventArgs;

namespace _0404_실습
{
    internal class MemberManager
    {
        #region 싱글톤
        public static MemberManager Instance { get; private set; }
        private MemberManager()
        {

        }
        static MemberManager()
        {
            Instance = new MemberManager();
        }
        #endregion

        #region 이벤트
        public event AddMemberEvent     AddMemberEventHandler       = null;
        public event SelectMemberEvent  SelectMemberEventHandler    = null;
        public event UpdateMemberEvent  UpdateMemberEventHandler    = null;
        public event DeleteMemberEvent  DeleteMemberEventHandler    = null;
        #endregion 

        private Dictionary<string, Member> members = new Dictionary<string, Member>();

        #region 기능

        public void PrintAll()
        {
            foreach(Member account in members.Values)
            {
                Console.WriteLine(account.ToString());
            }
        }
       
        public void AddMember()
        {
            try
            {
                Console.WriteLine();

                InputAddMember input = new InputAddMember();
                input.Input();
                
                Member member = new Member(input.Name, input.Addr);

                members.Add(input.Name, member);

                Console.WriteLine("저장 생성");

                //이벤트 발생
                if (AddMemberEventHandler != null) 
                {
                    AddMemberEventHandler(this, new AddMemberEventArgs(member));
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                Console.WriteLine("저장 실패");
            }
        }

        public void SelectMember()
        {
            try
            {
                Console.WriteLine();

                InputSelect input = new InputSelect();
                input.Input();

                Member member =  members[input.Name];

                member.Print();

                //이벤트 발생
                if (SelectMemberEventHandler != null)
                {
                    SelectMemberEventHandler(this, new SelectMemberEventArgs(input.Name));
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                Console.WriteLine("검색 실패");
            }
        }

        public void Update()
        {
            try
            {
                Console.WriteLine();

                InputUpdate update = new InputUpdate();
                update.Update();

                Member member = members[update.Name];
                member.Address = update.Addr;


                member.Print();

                //이벤트 발생
                if (UpdateMemberEventHandler != null)
                {
                    UpdateMemberEventHandler(this, new UpdateMemberEventArgs(update.Name));
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                Console.WriteLine("수정 실패");
            }
        }

        public void Delete()
        {
            try
            {
                Console.WriteLine();

                InputDelete delete = new InputDelete();
                delete.Delete();

                members.Remove(delete.Name);


                //이벤트 발생
                if (DeleteMemberEventHandler != null)
                {
                    DeleteMemberEventHandler(this, new DeleteMemberEventArgs(delete.Name));
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                Console.WriteLine("삭제 실패");
            }
        }

        #endregion     
    }
}
