using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace _0503_ChatService
{
    //델리게이트 선언
    public delegate void Chat(string nickaname, string msg, string type);

    public class ChatService : IChat
    {  
        //델게이트
        private Chat MyChat = null;
        
        //클라이언트 연결 인터페이스 
        IChatCallback callback = null;

        #region 1. Join(로그인하기)
        public bool Join(string nickaname)
        {
            //델리게이트
            MyChat = UserHandler; // MyChat = new Chat(UserHandler);

            //2. 클라이언트 연결 객체 생성
            callback = OperationContext.Current.GetCallbackChannel<IChatCallback>();

            //현재 접속자 정보를 모두에게 전달
            BroadcastMessage(nickaname, "", "UserEnter");
            return true;
        }
        #endregion

        #region 2. Say(메시지 보내기)
        public void Say(string nickaname, string msg)
        {
            BroadcastMessage(nickaname, msg, "Receive");
        }
        #endregion

        #region 3. Leave(로그아웃 하기)
        public void Leave(string nickaname)
        {
            //Chat d = null;
            //MyChat -= d;
            MyChat = null;
        }
        #endregion

        //
        private void BroadcastMessage(string nickaname, string msg, string msgType)
        {
            //MyChat에는 UserHandler 함수가 등록되어 있음.
            MyChat.BeginInvoke(nickaname, msg, msgType, new AsyncCallback(EndAsync), null);
        }

        private void UserHandler(string nickaname, string msg, string msgType)
        {
            try
            {
                //클라이언트에게 보내기
                switch (msgType)
                {
                    case "Receive":
                        callback.Receive(nickaname, msg);
                        break;
                    case "UserEnter":
                        callback.UserEnter(nickaname);
                        break;
                }
            }
            catch//에러가 발생했을 경우
            {
                Console.WriteLine("{0} 에러", nickaname);
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
            }
        }

    }
}