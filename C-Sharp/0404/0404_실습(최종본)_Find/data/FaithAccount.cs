
namespace _0404_실습
{
    class FaithAccount : Account
    {
        public FaithAccount(int id, string name, double balance) 
            : base(id, name, 1.01 * balance)
        {
        }

        public override void AddBalance(double add)
        {
            base.AddBalance(1.01 * add);
        }
    }
}
