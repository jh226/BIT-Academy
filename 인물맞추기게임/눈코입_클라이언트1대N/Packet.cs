using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace 눈코입_클라이언트
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
        
        public string Game_Start(string name)
        {
            string msg = null;
            msg += GAME_START + "@";
            msg += name;

            return msg;
        }

        public string Enter(string name)
        {
            string msg = null;
            msg += ENTER + "@";
            msg += name;

            return msg;
        }

        public string Chat(string name, string content)
        {
            string msg = null;
            msg += CHAT + "@";
            msg += name +"#";
            msg += content;

            return msg;
        }
    }
}
