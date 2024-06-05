using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace 눈코입_서버
{
    internal class Packet
    {
        //Client -> Server
        public const string ENTER = "Enter";
        public const string GAME_START = "Game_Start";
        public const string CHAT = "Chat";
        public const string RESTART = "Restart";


        //Server -> Client
        public const string ENTER_ACK = "ENTER_ACK";
        public const string GAME_START_ACK = "Game_Start_Ack";
        public const string CHAT_ACK = "Chat_Ack";
        public const string FULL_IMAGE_SEND = "Full_Image_Send";
        public const string IMAGE_SEND = "Image_Send";
        public const string GAME_WIN = "Game_Win";


        public string Enter(bool result, string name, int player)
        {
            string msg = null;
            msg += ENTER_ACK +"@";
            msg += result + "#";
            msg += name + "#";
            msg += player;

            return msg;
        }

        public string Game_Start_Ack()
        {
            string msg = null;
            msg += GAME_START_ACK + "@";

            return msg;
        }

        public string Chat(string name, string msg1)
        {
            string msg = null;
            msg += CHAT_ACK + "@";
            msg += name + "#";
            msg += msg1;

            return msg;
        }

        public string Game_Win(string name)
        {
            string msg = null;
            msg += GAME_WIN + "@";
            msg += name;

            return msg;
        }

        public string Full_Image_Send(byte[] arr1, string answer)
        {
            string msg = null;
            msg += FULL_IMAGE_SEND + "@";
            msg += Convert.ToBase64String(arr1) +"#";
            msg += answer;

            return msg;
        }

        public string Image_Send(byte[] arr1, byte[] arr2, byte[] arr3, byte[] arr4)
        {
            string msg = null;
            msg += IMAGE_SEND + "@";
            msg += Convert.ToBase64String(arr1) + "#";
            msg += Convert.ToBase64String(arr2) + "#";
            msg += Convert.ToBase64String(arr3) + "#";
            msg += Convert.ToBase64String(arr4);

            return msg;
        }
    }
}
