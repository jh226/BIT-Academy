using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using static System.Net.WebRequestMethods;

namespace _0503_chat
{
    internal class ChatService :IChat
    {
        private WbDB db = WbDB.Instance;
        //델리게이트 선언
        public delegate void Chat(string id, int idx, string msg, string type);
        public delegate void Data(string name, int idx, string msg, byte[] filedata, string type);

        //동기화 작업을 위해서 가상의 객체 생성
        private static Object syncObj = new Object();

        //채팅방에 있는 유저 이름 목록
        private static ArrayList Chatter = new ArrayList();
        private Data MyFile;

        //델리게이트 =========================================================
        // 개인용 델리게이트
        private Chat MyChat;

        //전체에게 보낼 정보를 담고 있는 델리게이트
        private static Chat List;
        IChatCallback callback = null;
        
        #region 생성자
        public ChatService()
        {
            if (db.Open() == true)
            {
                Console.WriteLine("데이터 베이스에 연결되었습니다.....");
            }
        }
        #endregion

        #region 1. 로그인하기
        public bool Join(string id, int idx)
        {
            
            MyChat = new Chat(UserHandler);
            lock (syncObj)
            {

                if (!Chatter.Contains(idx)) //이름이 기존 채터에 있는지 검색한다.
                {
                    //2. 사용자에게 보내 줄 채널을 설정한다.
                    callback = OperationContext.Current.GetCallbackChannel<IChatCallback>();

                    //현재 접속자 정보를 모두에게 전달
                    BroadcastMessage(id, idx, "", "UserEnter");

                    //델리게이터 추가(향후 데이터 수신이 가능하도록 구성)
                    List += MyChat;

                    db.UpdateMember(id, idx, true);

                    return true;
                }
                return false;
            }
        }

        public StudentData[] Join1(string id, int idx)
        {
            MyChat = new Chat(UserHandler); 
            lock (syncObj)
            {
                if (!Chatter.Contains(id))
                { // 이름이 기존 채터에 있는지 검색한다. //1. 로그인 데이터 처리 ========
                    StudentData data = new StudentData(true, id, idx); 
                    Chatter.Add(data);
                    //2. 사용자에게 보내 줄 채널을 설정한다.
                    callback = OperationContext.Current.GetCallbackChannel<IChatCallback>();
                    //현재 접속자 정보를 모두에게 전달
                    BroadcastMessage(id, idx, "", null, "UserEnter");

                    //델리게이터 추가(향후 데이터 수신이 가능하도록 구성)
                    List += MyChat;
                    
                        //사용자리스트를 보내준다.
                    StudentData[] list = new StudentData[Chatter.Count]; lock (syncObj)
                    {
                        lock (syncObj) { list[0] = data; }
                    }
                    db.UpdateMember(id, idx, true);
                    return list;

                }
                else
                { // 이미 사용자가 사용하고 있는 이름일 경우
                    return null;
                }
            }
        }
        #endregion

        #region 2. 채팅, 파일 보내기
        public void Say(string id, int idx, string msg)
        {
            BroadcastMessage(id, idx, msg, "Receive");
            bool b;
            if(msg.Length<50)
                b = db.InsertShortmessage(id, msg);
            else
                b = db.InsertLongmessage(id, msg);
            Console.WriteLine("채팅저장 결과 : {0}", b);
        }

        public bool UpLoadFile(string id, int idx, string filename, byte[] data)
        {
            {
                try
                {
                    FileStream writeFileStream = new FileStream(@"C:\00_Files\" + filename, FileMode.Create, FileAccess.Write);
                    BinaryWriter dataWriter = new BinaryWriter(writeFileStream);

                    dataWriter.Write(data);
                    writeFileStream.Close();
                    BroadcastMessage(id, idx, filename, data, "FileSender");

                    db.InsertFileMessage(id, filename);

                    return true;
                }
                catch
                {
                    return false;
                }
            }
        }
        #endregion

        #region 3. 로그아웃 하기
        public void Leave(string id, int idx)
        {
            //메시지 수신에서 제외
            List -= MyChat;

            //모든 사람에게 전송
            string msg = string.Format(idx + "이가 나갔습니다");
            BroadcastMessage(id, idx, msg, "UserLeave");
            db.UpdateMember(id, idx, false);
        }

        public void Leave1(string id, int idx)
        {
            if (id == null) return;

            lock(syncObj)
            {
                foreach(StudentData data in Chatter)
                {
                    if(data.Name == id)
                    {
                        Chatter.Remove(data);
                        break;
                    }
                }
            }

            //메시지 수신에서 제외
            List -= MyChat;

            //모든 사람에게 전송
            string msg = string.Format(idx + "이가 나갔습니다");
            BroadcastMessage(id, idx, msg, "UserLeave");
            db.UpdateMember(id, idx, false);
        }
        #endregion


        private void BroadcastMessage(string id, int idx, string msg, string msgType)
        {
            if (List != null)
            {
                //현재 이벤트들을 전달한다.
                foreach (Chat handler in List.GetInvocationList())
                {
                    handler.BeginInvoke(id, idx, msg, msgType, new AsyncCallback(EndAsync), null);
                }
            }
        }

        private void BroadcastMessage(string name, int idx, string msg, byte[] filedata, string msgType)
        {
            if (List != null)
            {
                foreach (Data handler in List.GetInvocationList())
                {
                    if (handler == MyFile && msgType == "FileSender")
                        continue;
                    handler.BeginInvoke(name, idx, msg, filedata, msgType, new AsyncCallback(EndAsync), null);
                }
            }
        }

        private void UserHandler(string id, int idx, string msg, string msgType)
        {
            try
            {
                //클라이언트에게 보내기
                switch (msgType)
                {
                    case "Receive":
                        callback.Receive(id, idx, msg);
                        break;
                    case "UserEnter":
                        callback.UserEnter(id, idx);
                        break;
                    case "UserLeave":
                        callback.UserLeave(id, idx);
                        break;
                }
            }
            catch//에러가 발생했을 경우
            {
                Leave(id, idx);
            }
        }

        private void EndAsync(IAsyncResult ar)
        {
            Chat d = null;
            try
            {
                System.Runtime.Remoting.Messaging.AsyncResult asres = (System.Runtime.Remoting.Messaging.AsyncResult)ar;
                d = ((Chat)asres.AsyncDelegate);
                d.EndInvoke(ar);
            }
            catch
            {
                List -= d;
            }

        }

        
    }
}
