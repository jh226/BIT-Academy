using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _0406_Server
{
    internal static class Packet
    {
        //Client -> Server

        public const string GAME_START = "Game_Start";
        public const string GAME_PAUSE = "Game_Pause";
        public const string GAME_NUM   = "Game_Num";

        //Server -> Client

        public const string GAME_START_ACK = "Game_Start";
        public const string GAME_PAUSE_ACK = "Game_Pause";
        public const string GAME_NUM_ACK   = "Game_Num";




        public static string Game_Start_Ack(bool b, string name)
        {
            string msg = null;
            msg += GAME_START_ACK + "@";
            msg += b + "#";
            msg += name;

            return msg;
        }

        public static string Game_Pause_Ack(bool b, string name)
        {
            string msg = null;
            msg += GAME_PAUSE_ACK + "@";
            msg += b + "#";
            msg += name;

            return msg;
        }

        public static string Game_Num(string name, int num)
        {
            string msg = null;
            msg += GAME_NUM + "@";
            msg += name + "#";
            msg += num;


            return msg;
        }

    }
}
