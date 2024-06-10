using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _0406_Server
{
    internal class Account
    {
        public int Id { get; private set; }
        public string Name { get; private set; }
        public int Balance { get; private set; }

        #region  생성자 
        public Account(int id, string name, int balance)
        {
            this.Id = id;
            this.Name = name;
            this.Balance = balance;
        }

        #endregion

        #region 메서드

        public virtual void AddBalance(int add)    //잔액입금
        {
            if (add < 0)
                throw new Exception("잘못된 금액 입금");
            Balance += add;
        }
        public void MinBalance(int min)    //잔액출금
        {
            if (min > Balance)
            {
                string msg = string.Format("요청금액 : {0}, 현재잔액 : {1} > 잔액부족", min, Balance);
                throw new Exception(msg);
            }
            if (min < 0)
                throw new Exception("잘못된 금액 출금 요청");

            Balance -= min;
        }
        public virtual void ShowAllData()
        {
            Console.WriteLine("***** 계좌 정보 *****");
            Console.WriteLine("계좌번호 : {0}", Id);
            Console.WriteLine("성    함 : {0}", Name);
            Console.WriteLine("잔    액 : {0}", Balance);
        }

        #endregion

        #region object클래스 Override

        public override string ToString()
        {
            return Id + "\t" + Name + "\t" + Balance;
        }
        #endregion
    }
}
