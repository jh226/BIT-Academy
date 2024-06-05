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
        public const string MAKE_ACCOUNT       = "Make_Account";
        public const string DEPOSIT_ACCOUNT    = "Deposit_Account";
        public const string WITHDRAW_ACCOUNT   = "Withdraw_Account";
        public const string INQUIRE_ACCOUNT    = "Inquire_Account";

        //Server -> Client
        public const string MAKE_ACCOUNT_ACK    = "Make_Account";
        public const string DEPOSIT_ACCOUNT_ACK = "Deposit_Account";

        public static string MakeAccount_Ack(bool b, int id)
        {
            string msg = null;
            msg += MAKE_ACCOUNT_ACK + "@";
            msg += b + "#";
            msg += id;

            return msg;
        }

        public static string DepositAccount_Ack(bool b, int id, int balance)
        {
            string msg = null;
            msg += DEPOSIT_ACCOUNT_ACK + "@";
            msg += b + "#";
            msg += id + "#";
            msg += balance;

            return msg;
        }        
    }
}
