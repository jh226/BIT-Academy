using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace _0503_ChatService
{
    #region 1. 메세지 관련 Contract InterFace (클라이언트->서버)
    [ServiceContract(SessionMode = SessionMode.Required, CallbackContract = typeof(IChatCallback))]
    public interface IChat
    {
        [OperationContract(IsOneWay = false, IsInitiating = true, IsTerminating = false)]
        bool Join(string nickaname);

        [OperationContract(IsOneWay = true, IsInitiating = false, IsTerminating = false)]
        void Say(string nickaname, string msg);

        [OperationContract(IsOneWay = true, IsInitiating = false, IsTerminating = true)]
        void Leave(string nickaname);
    }
    #endregion


    #region 2. 클라이언트에 콜백할 CallBackContract  (서버->클라이언트)
    public interface IChatCallback
    {
        [OperationContract(IsOneWay = true)]
        void Receive(string nickaname, string message);

        [OperationContract(IsOneWay = true)]
        void UserEnter(string nickaname);
    }
    #endregion
}
