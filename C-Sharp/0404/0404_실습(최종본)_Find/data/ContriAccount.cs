using System;

namespace _0404_실습
{
    class ContriAccount : Account
    {
        public double Contribution { get; private set; }

        #region 생성자
        public ContriAccount(int id, string name, double balance) 
            : base(id, name, 0.99 * balance)
        {
            Contribution = 0.01 * balance;
        }
        #endregion

        #region 메서드
        public override void AddBalance(double add)
        {
            try
            {
                base.AddBalance(0.99 * add);
                Contribution += 0.01 * add;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }

        public override void ShowAllData()
        {
            base.ShowAllData();
            Console.WriteLine("기부내역 : " + Contribution);
        }
        #endregion

        #region Account클래스 Override

        public override string ToString()
        {
            return base.ToString() + "\t" + Contribution;
        }

        #endregion 
    }
}

