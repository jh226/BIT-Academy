using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace _0503_chat
{
    #region 1. 메세지 관련 Contract InterFace (클라이언트->서버)
    [ServiceContract(SessionMode = SessionMode.Required, CallbackContract = typeof(IChatCallback))]
    public interface IChat
    {
        [OperationContract(IsOneWay = false, IsInitiating = true, IsTerminating = false)]
        bool Join(string id, int idx);

        [OperationContract(IsOneWay = false, IsInitiating = true, IsTerminating = false)]
        StudentData[] Join1(string id, int idx);

        [OperationContract(IsOneWay = true, IsInitiating = false, IsTerminating = false)]
        void Say(string id, int idx, string msg);

        [OperationContract(IsOneWay = true, IsInitiating = false, IsTerminating = true)]
        void Leave(string id, int idx);
        
        [OperationContract]
        void Leave1(string id, int idx);

        [OperationContract]
        bool UpLoadFile(string name, int idx, string filename, byte[] data);

    }
    #endregion

    #region 2. 클라이언트에 콜백할 CallBackContract  (서버->클라이언트)
    public interface IChatCallback
    {
        [OperationContract(IsOneWay = true)]
        void Receive(string id, int idx, string message);

        [OperationContract(IsOneWay = true)]
        void UserEnter(string id, int idx);

        [OperationContract(IsOneWay = true)]
        void UserLeave(string id, int idx);

        [OperationContract(IsOneWay = true)]
        void FileRecive(string sendername, int idx, string filename, byte[] filedata);
    }
    #endregion

}
