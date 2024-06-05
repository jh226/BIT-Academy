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
        public const string PRINT_ALL          = "Print_All";
        public const string MAKE_ACCOUNT       = "Make_Account";
        public const string DEPOSIT_ACCOUNT    = "Deposit_Account";
        public const string WITHDRAW_ACCOUNT   = "Withdraw_Account";
        public const string INQUIRE_ACCOUNT    = "Inquire_Account";
        public const string DELETE_ACCOUNT     = "Delete_Account";

        //Server -> Client
        public const string PRINT_ALL_ACK = "Print_All";
        public const string MAKE_ACCOUNT_ACK    = "Make_Account";
        public const string DEPOSIT_ACCOUNT_ACK = "Deposit_Account";
        public const string WITHDRAW_ACCOUNT_ACK = "Withdraw_Account";
        public const string INQUIRE_ACCOUNT_ACK = "Inquire_Account";
        public const string DELETE_ACCOUNT_ACK = "Delete_Account";

        public static string Print_All_ACK(bool b,  List<Account> accounts)
        {
            string msg = null;

            for(int i=0; i<accounts.Count; i++)
            {
                msg += PRINT_ALL_ACK + "@";
                msg += b + "#";
                msg += accounts[0].Id + "#";
                msg += accounts[0].Name + "#";
                msg += accounts[0].Balance + "*";
            }

            //msg += MAKE_ACCOUNT_ACK + "@";
            //msg += b + "#";
            //msg += id;

            return msg;
        }

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
        public static string WithdrawAccount_Ack(bool b, int id, int balance)
        {
            string msg = null;
            msg += DEPOSIT_ACCOUNT_ACK + "@";
            msg += b + "#";
            msg += id + "#";
            msg += balance;

            return msg;
        }
        public static string InquireAccount_Ack(bool b, int id, string name, int balance)
        {
            string msg = null;
            msg += INQUIRE_ACCOUNT_ACK + "@";
            msg += b + "#";
            msg += id + "#";
            msg += name + "#";
            msg += balance;
            return msg;
        }
        public static string DeleteAccount_Ack(bool b, int id)
        {
            string msg = null;
            msg += DELETE_ACCOUNT_ACK + "@";
            msg += b + "#";
            msg += id;
            return msg;
        }
    }
}
