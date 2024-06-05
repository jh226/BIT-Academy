using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wb.Net
{
    internal static class Packet
    {
        //Client -> Server
        public const string MAKE_ACCOUNT        = "Make_Account";
        public const string DEPOSIT_ACCOUNT     = "Deposit_Account";
        public const string WITHDRAW_ACCOUNT    = "Withdraw_Account";
        public const string INQUIRE_ACCOUNT     = "Inquire_Account";

        //Server -> Client
        public const string MAKE_ACCOUNT_ACK    = "Make_Account";
        public const string DEPOSIT_ACCOUNT_ACK = "Deposit_Account";

        public static string MakeAccount(int id, string name, int balance)
        {
            string msg = null;
            msg += MAKE_ACCOUNT + "@";      // 개설 요청 메시지
            msg += id + "#";                  // 아이디
            msg += name + "#";                // 이름
            msg += balance;                   // 잔액

            return msg;
        }

        public static string DepositAccount(int id, int balance)
        {
            string msg = null;
            msg += DEPOSIT_ACCOUNT + "@";     // 입금 요청 메시지
            msg += id + "#";                  // 아이디
            msg += balance;                   // 잔액

            return msg;
        }

        public static string WithdrawAccount(int id, int balance)
        {
            string msg = null;
            msg += WITHDRAW_ACCOUNT + "@";    // 출금 요청 메시지
            msg += id + "#";                  // 아이디
            msg += balance;                   // 잔액

            return msg;
        }

        public static string InquireAccount(int id)
        {
            string msg = null;
            msg += INQUIRE_ACCOUNT + "@";       // 검색 요청 메시지
            msg += id;                          // 아이디

            return msg;
        }
        
    }
}
