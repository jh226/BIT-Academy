using System;
using System.Collections.Generic;

namespace _0404_실습
{
    public class Account : IComparable<Account>
    {
        public int Id           { get; private set; }
        public string Name      { get; private set; }
        public double Balance { get; set; }

        #region  생성자 
        public Account(int id, string name, double balance)
        {
            this.Id = id;
            this.Name = name;
            this.Balance = balance;
        }

        #endregion

        #region 메서드

        public virtual void AddBalance(double add)    //잔액입금
        {
            if (add < 0)
                throw new Exception("잘못된 금액 입금");
            Balance += add;
        }
        public void MinBalance(double min)    //잔액출금
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

        public int CompareTo(Account other)
        {
            return Id - other.Id;  //1, 0, -1
        }
    }

    //[Account] 잔액 정렬 클래스
    class Sort_Balance : IComparer<Account>
    {
        public int Compare(Account x, Account y)
        {
            //return (int)(x.Balance - y.Balance);  //오름차순
            return (int)(y.Balance - x.Balance);    //내림차순
        }
    }

    //[Account] 계좌번호로 검색 클래스
    class Find_AccID
    {
        public int AccID { get; private set; }

        public Find_AccID(int accid)
        {
            AccID = accid;
        }

        public bool FindAccID(Account obj)
        {
            return obj.Id == AccID;
        }
    }
}

