using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _0502_WCFService1
{
    //2. 서비스 객체
    internal class HelloWorldWCFService : IHelloWorld
    {
        //의미가 없다!
        static List<Product> products = new List<Product>();

        public bool Insert_Product(int id, string name)
        {
            products.Add(new Product(id, name));
            return true;
        }

        public int Product_Count()
        {
            return products.Count();
        }

        public List<Product> GetList_Product()
        {
            return products;
        }

        public string SayHello()
        {
            return "Hello WCF World !";
        }
    }
}
